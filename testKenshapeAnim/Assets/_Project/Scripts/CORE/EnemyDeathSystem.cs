using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class EnemyDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Enemy, Dead> _deathFilter = null;
        
        public void Run()
        {
            foreach (var index in _deathFilter)
            {
                ref var enemyEntity = ref _deathFilter.GetEntity(index);

                Object.Destroy(enemyEntity.Get<Enemy>().View);
                enemyEntity.Destroy();
            }
        }
    }
}