using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace BombGame
{
    internal class SpawnEnemySystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly GameData _data = null;
        private readonly References _refs = null;
        private readonly Configuration _config = null;

        private readonly EcsFilter<Spawner> _spawnerFilter = null;

        private Vector3 _offset = new Vector3(0, 10, 0);
      
        public void Run()
        {
            if (_data.GameState != State.Gameplay) return;
            if (_data.GameplayTimer <= 0) return;

            foreach (var index in _spawnerFilter)
            {
                ref var spawner = ref _spawnerFilter.Get1(index);

                if (spawner.Timer > 0)
                {
                    spawner.Timer -= Time.deltaTime;
                }
                else
                {
                    var enemyEntity = _world.NewEntity();
                    
                    ref var enemyView = ref enemyEntity.Get<Enemy>().View;
                    ref var enemyHealth = ref enemyEntity.Get<Health>();

                    enemyView = Object.Instantiate(_refs.EnemyPrefab, spawner.Position + _offset, Quaternion.Euler(new Vector3(0,180,0)));
                    spawner.Timer = _config.SpawnTimerDefault;

                    enemyHealth.CurrentHealth = _config.EnemyHealth;
                    ref var healthCanvas = ref enemyEntity.Get<HealthCanvas>();

                    healthCanvas.HealthImage =
                        enemyView.GetComponentInChildren<FillTag>().gameObject.GetComponent<Image>();

                }
            }
        }
    }
}