using UnityEngine;
using Leopotam.Ecs;

namespace Client
{
    internal class EnemyDeathSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy> _filter;
        public void Run()
        {
            if(!_filter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    var enemy = _filter.GetEntity(index);
                    var _health = enemy.Get<Enemy>().currentHealth;

                    if(_health <= 0)
                    {
                        GameObject.Destroy(enemy.Get<Enemy>().avatar.gameObject);
                        enemy.Destroy();
                    }
                }
            }
        }
    }
}