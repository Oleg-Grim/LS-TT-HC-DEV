using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Client
{
    internal class SpawnEnemySystem : IEcsRunSystem
    {
        private EcsFilter<Spawner, Position> _filter;
        public EcsWorld _world;
        private Configuration _config;
        private float spawnTimer = 0;

        public void Run()
        {
            if(_config.gameState == Configuration.GameState.gameplay)
            {
                if (_config.gameTimer > 0)
                {
                    foreach (var index in _filter)
                    {
                        if (spawnTimer < 0)
                        {
                            var spawner = _filter.GetEntity(index);
                            var spawnerFX = GameObject.Instantiate(spawner.Get<SpawnerFX>().spawnerFX);
                            spawnerFX.transform.position = _filter.Get2(index).value;

                            var enemy = _world.NewEntity();

                            enemy.Get<ActiveEnemy>();
                            var enemyPrefab = enemy.Get<Enemy>().avatar = _config.enemyPrefab;
                            var position = enemy.Get<Enemy>().position = _filter.Get2(index).value;
                            var tmp = GameObject.Instantiate(enemyPrefab);

                            tmp.transform.position = position;

                            enemy.Get<Enemy>().maxHealth = _config.enemyDefaultHealth + (_config.enemyHealthMultiplier * (_config.levelCount - 1));
                            enemy.Get<Enemy>().currentHealth = enemy.Get<Enemy>().maxHealth;
                            enemy.Get<Enemy>().speed = 5f;
                            enemy.Get<Enemy>().avatar = tmp;
                            enemy.Get<Enemy>().spawnerRef = _filter.GetEntity(index);

                            spawnTimer = 5;
                        }
                        else
                        {
                            spawnTimer -= Time.deltaTime;
                        }
                    }
                }
            }
        }

    }
}