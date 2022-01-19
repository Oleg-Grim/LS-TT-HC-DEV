using Leopotam.Ecs;

namespace BombGame
{
    internal class TrackEnemyHealthSystem : IEcsRunSystem
    {
        private readonly GameData _data = null;
        private readonly Configuration _config = null;
        
        private readonly EcsFilter<Health, HealthCanvas> _enemyHealthFilter = null;

        public void Run()
        {
            if (_data.GameState != State.Gameplay) return;

            foreach (var index in _enemyHealthFilter)
            {
                ref var enemyEntity = ref _enemyHealthFilter.GetEntity(index);
                ref var enemyHealth = ref _enemyHealthFilter.Get1(index);
                ref var enemyCanvas = ref _enemyHealthFilter.Get2(index);
                
                enemyCanvas.HealthImage.fillAmount = enemyHealth.CurrentHealth / _config.EnemyHealth;

                if (enemyHealth.CurrentHealth <= 0)
                {
                    enemyEntity.Get<Dead>();
                    CEvents.FireEnemyKilled();
                }
            }
        }
    }
}
