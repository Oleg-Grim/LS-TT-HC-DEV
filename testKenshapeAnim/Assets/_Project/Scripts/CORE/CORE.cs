using Leopotam.Ecs;
using UnityEngine;

namespace BombGame {
    sealed class CORE : MonoBehaviour {
        EcsWorld _world;
        EcsSystems _systems;

        public GameData _data;
        public References _refs;
        public Configuration _config;

        void Start () {
            
            _world = new EcsWorld ();
            _systems = new EcsSystems (_world);
#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create (_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create (_systems);
#endif

            _config = SaveSystem.Load();
            
            AddSystems();
            AddOneFrames();
            AddInjects();
            
            _systems.Init();
        }

        private void AddSystems()
        {
            _systems
                .Add(new InitGameplaySystem())
                .Add(new GameStateSystem())
                .Add(new GameplayTimerSystem())
                
                .Add(new SpawnBombSystem())
                .Add(new MoveBombSystem())
                .Add(new HandleExplosionsSystem())
                
                .Add(new SpawnEnemySystem())
                .Add(new HandleEnemyMovementSystem())
                .Add(new EnemyHealthCanvasSystem())
                .Add(new TrackEnemyHealthSystem())
                .Add(new EnemyDeathSystem())
                
                ;
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<GameplayScreen>()
                .OneFrame<FinishScreen>()
                .OneFrame<MainMenuScreen>()
                .OneFrame<ShopScreen>()
                .OneFrame<DamageCheck>()
                .OneFrame<IncreaseLevel>()
                .OneFrame<AddScore>()
                
                .OneFrame<Win>()
                // .OneFrame<Lose>()
                
                ;
        }

        private void AddInjects()
        {
            _systems
                .Inject(_data)
                .Inject(_refs)
                .Inject(_config)

                ;
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