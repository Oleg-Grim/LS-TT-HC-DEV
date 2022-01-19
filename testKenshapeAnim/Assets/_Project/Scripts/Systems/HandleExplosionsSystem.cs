using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class HandleExplosionsSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly Configuration _config = null;
        private readonly References _refs = null;
        
        private readonly EcsFilter<Bomb> _bombFilter = null;
        private readonly EcsFilter<Enemy, Health> _enemyFilter = null;
        
        public void Run()
        {
            if(!_bombFilter.IsEmpty())
            {
                foreach (var index in _bombFilter)
                {
                    ref var bombEntity = ref _bombFilter.GetEntity(index);
                    ref var bomb = ref _bombFilter.Get1(index);
                    
                    if(!(bomb.View.transform.position.y < 0.5f)) continue;
                    
                    var explosionEntity = _world.NewEntity();
                    ref var explosion = ref explosionEntity.Get<Explosion>();
                    
                    explosion.View = Object.Instantiate(_refs.BombFX);
                    explosion.View.transform.localScale = Vector3.one * _config.BombRadius/2;
                    explosion.View.transform.position = bomb.View.transform.position;

                    Object.Destroy(bomb.View);
                    bombEntity.Destroy();

                    foreach (var enemyIndex in _enemyFilter)
                    {
                        ref var enemyView = ref _enemyFilter.Get1(enemyIndex).View;
                        ref var enemyHealth = ref _enemyFilter.Get2(enemyIndex);

                        var enemyPosition = new Vector3(enemyView.transform.position.x, 0,
                            enemyView.transform.position.z);
                        
                        var bombPosition = new Vector3(explosion.View.transform.position.x, 0,  explosion.View.transform.position.z);

                        float distance = Vector3.Distance(enemyPosition, bombPosition);

                        if (distance < _config.BombRadius)
                        {
                            enemyHealth.CurrentHealth -= DealDamage(distance);
                        }
                    }
                }
            }
        }

        private float DealDamage(float distance)
        {
            float damage = _config.BombDamage * (100 - (distance / (_config.BombRadius / 100))) /100;
            return damage;
        }
    }
}