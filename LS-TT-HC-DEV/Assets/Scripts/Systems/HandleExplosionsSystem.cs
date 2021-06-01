using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class HandleExplosionsSystem : IEcsRunSystem
    {
        private EcsWorld _world;
        private EcsFilter<Bomb, Explode> _bombFilter;
        private EcsFilter<Enemy> _enemyFilter;
        private Configuration _config;
        public void Run()
        {
            if(!_bombFilter.IsEmpty())
            {
                foreach (var index in _bombFilter)
                {
                    var bomb = _bombFilter.GetEntity(index);
                    var explosion = _world.NewEntity();
                    explosion.Get<ExplosionCheck>().position = bomb.Get<Bomb>().avatar.transform.position;
                    explosion.Get<ExplosionCheck>().bombFX = _config.bombFX;
                    explosion.Get<ExplosionCheck>().bombFX.transform.localScale = new Vector3(_config.bombRadius / 2, _config.bombRadius / 2, _config.bombRadius / 2);
                    
                    var kaboom = GameObject.Instantiate(explosion.Get<ExplosionCheck>().bombFX);
                    kaboom.transform.position = bomb.Get<Bomb>().position;

                    GameObject.Destroy(bomb.Get<Bomb>().avatar.gameObject);
                    bomb.Destroy();

                    foreach (var enemyIndex in _enemyFilter)
                    {
                        var enemy = _enemyFilter.GetEntity(enemyIndex);

                        Vector2 enemyPosition = new Vector2(_enemyFilter.Get1(enemyIndex).avatar.transform.position.x, _enemyFilter.Get1(enemyIndex).avatar.transform.position.z);
                        Vector2 bombPosition = new Vector2(explosion.Get<ExplosionCheck>().position.x, explosion.Get<ExplosionCheck>().position.z);

                        float distance = Vector2.Distance(enemyPosition, bombPosition);

                        if (distance < _config.bombRadius)
                        {
                            enemy.Get<Enemy>().spawnerRef.Del<HasActiveEnemy>();
                            enemy.Get<Enemy>().currentHealth -= DealDamage(distance);
                            Debug.Log(DealDamage(distance));
                        }
                    }
                    
                    explosion.Destroy();

                }
            }
        }

        private float DealDamage(float distance)
        {
            float damage = _config.bombDamage * (100 - (distance / (_config.bombRadius / 100))) /100;
            return damage;
        }
    }
}