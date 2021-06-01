using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    public class LevelReset : IEcsRunSystem
    {
        private Configuration _config;
        private EcsWorld _world;
        private EcsFilter<PlayState> _filter;
        private UI_Controller UIC;

        public void Run()
        {
            if(UIC.continueButton == EcsEntity.Null)
            {

                var monitor = _world.NewEntity();
                monitor.Get<PlayState>();

                UIC.continueButton = monitor;
            }

            foreach (var index in _filter)
            {
                var stateCheck = _filter.GetEntity(index);

                if(stateCheck.Has<WinState>())
                {
                    if(_config.diffCounter > 1)
                    {
                        _config.diffCounter--;
                    }
                    else
                    {
                        _config.difficultySpawners++;
                        _config.diffCounter = _config.diffDefaultCounter;
                    }

                    _config.levelCount++;
                    _config.gameTimer = _config.gameDefaultTimer;

                    foreach (var item in _config.ecsEntities)
                    {
                        item.Destroy();
                    }

                    _config.resetGame = true;
                    _config.gameState = Configuration.GameState.gameplay;
                }

                if(stateCheck.Has<LoseState>())
                {
                    _config.diffCounter = _config.diffDefaultCounter;
                    _config.difficultySpawners = _config.difficultyDefaultSpawners;
                    _config.levelCount = _config.levelDefaultCount;
                    _config.gameTimer = _config.gameDefaultTimer;

                    foreach (var item in _config.ecsEntities)
                    {
                        item.Destroy();
                    }

                    _config.resetGame = true;
                    _config.gameState = Configuration.GameState.mainMenu;
                }
            }
        }
    }
}