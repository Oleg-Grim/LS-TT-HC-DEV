using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    sealed class EcsStartup : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public Configuration configuration = new Configuration();
        public UI_Controller UIC;


        void Start () {
            // void can be switched to IEnumerator for support coroutines.
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);


#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif
            _systems
                // register your systems here, for example:
                // .Add (new TestSystem1 ())
                .Add(new WorldSetUpSystem())
                .Add(new SpawnEnemySystem())
                .Add(new EnemySystem())
                .Add(new SpawnPlayerBombSystem())
                .Add(new BombDropSystem())
                .Add(new HandleExplosionsSystem())
                .Add(new GameLoopSystem())
                .Add(new LevelReset())
                .Add(new EnemyDeathSystem())
                .Add(new EnemyHealthbarSystem())
                // register one-frame components (order is important), for example:
                // .OneFrame<TestComponent1> ()
                // .OneFrame<TestComponent2> ()
                .OneFrame<Explode>()
                .OneFrame<ExplosionCheck>()
                .OneFrame<WinState>()
                .OneFrame<LoseState>()

                // inject service instances here (order doesn't important), for example:
                // .Inject (new CameraService ())
                // .Inject (new NavMeshSupport ())
                .Inject(configuration)
                .Inject(UIC)
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            if (_systems != null) {
                _systems.Destroy ();
                _systems = null;
                _world.Destroy ();
                _world = null;
            }
        }
    }
}