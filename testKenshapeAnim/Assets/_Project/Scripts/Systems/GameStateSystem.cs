using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class GameStateSystem : IEcsRunSystem
    {
        private readonly GameData _data = null;
        private readonly Configuration _config = null;
        private readonly References _refs = null;
        
        private readonly EcsFilter<Control> _controlFilter = null;
        private readonly EcsFilter<Enemy> _enemyFilter = null;
        private readonly EcsFilter<Explosion> _explosionFilter = null;
        
        public void Run()
        {
            foreach (var index in _controlFilter)
            {
                ref var control = ref _controlFilter.GetEntity(index);

                if (control.Has<GameplayScreen>())
                {
                    _data.GameState = State.Gameplay;
                    _data.GameplayTimer = _config.LevelTimerDefault;
                    CEvents.FireTimerChanged("30");
                }

                if (control.Has<ShopScreen>())
                {
                    _data.GameState = State.Shop;
                    ClearEntities();
                }

                if (control.Has<Lose>())
                {
                    CEvents.FireLoseGame(_config.CurrentLevel);
                    _refs._uiCore.SwapMenu(3);
                    _data.GameState = State.Finish;
                    control.Del<Lose>();
                }

                if (_enemyFilter.IsEmpty() && _data.GameplayTimer <= 0 && _data.GameState == State.Gameplay)
                {
                    CEvents.FireWinGame(_config.CurrentLevel);
                    _config.EnemyHealth += 1;
                    _data.GameState = State.Finish;
                    control.Get<Win>();
                }

                if (control.Has<AddScore>())
                {
                    ref var score = ref control.Get<AddScore>().Score;
                    _data.LevelMoney += score;
                    _config.PlayerMoney += _data.LevelMoney;
                    _data.LevelMoney = 0;
                }

                if (control.Has<IncreaseLevel>())
                {
                    _config.CurrentLevel += 1;
                    _config.EnemyHealth += 1;
                    _config.SpawnTimerDefault -= 0.1f;
                }

                if (control.Has<MainMenuScreen>())
                {
                    ClearEntities();
                }
            }
        }

        private void ClearEntities()
        {
            foreach (var idx in _enemyFilter)
            {
                ref var enemyEntity = ref _enemyFilter.GetEntity(idx);
                ref var enemyView = ref _enemyFilter.Get1(idx);

                Object.Destroy(enemyView.View);
                enemyEntity.Destroy();
            }

            foreach (var index in _explosionFilter)
            {
                ref var explosionEntity = ref _explosionFilter.GetEntity(index);
                explosionEntity.Destroy();
            }
        }
    }
}