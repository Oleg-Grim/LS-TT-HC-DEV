using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.UI;

namespace Client
{
    internal class GameLoopSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, HasReachedEnd> _loseFilter;
        private EcsFilter<Enemy> _endFilter;
        private Configuration _config;
        private UI_Controller UIC;
        private bool init = false;

        public void Run()
        {
            if(_config.gameTimer > 0)
            {
                UIC.timerText.text = _config.gameTimer.ToString("##");
            }
            else
            {
                UIC.timerText.text = "Bomb the rest of them!";
            }

            UIC.levelText.text = ("Level: " + _config.levelCount.ToString());
                
            if(!init)
            {
                _config.gameState = Configuration.GameState.mainMenu;
                init = true;
            }
            
            switch (_config.gameState)
            {
                case Configuration.GameState.mainMenu:
                    {
                        UIC.TurnUI(0);

                        if (Input.GetMouseButtonDown(0))
                        {
                            _config.gameState = Configuration.GameState.gameplay;
                        }
                    }
                    break;
                case Configuration.GameState.gameplay:
                    {
                        UIC.TurnUI(1);
                        _config.gameTimer -= Time.deltaTime;
                        
                        if (!_loseFilter.IsEmpty())
                        {
                            _config.gameState = Configuration.GameState.loseScreen;
                        }

                        if (_endFilter.IsEmpty() && _config.gameTimer <=0)
                        {
                            _config.gameState = Configuration.GameState.WinScreen;
                        }
                    }
                    break;
                case Configuration.GameState.loseScreen:
                    {
                        UIC.TurnUI(3);

                        foreach (var enemyIndex in _endFilter)
                        {
                            var enemy = _endFilter.GetEntity(enemyIndex);
                            enemy.Get<Enemy>().spawnerRef.Del<HasActiveEnemy>();
                            GameObject.Destroy(enemy.Get<Enemy>().avatar.gameObject);
                            enemy.Destroy();
                        }
                    }
                    break;
                case Configuration.GameState.WinScreen:
                    {
                        UIC.TurnUI(2);

                        foreach (var enemyIndex in _endFilter)
                        {
                            var enemy = _endFilter.GetEntity(enemyIndex);
                            enemy.Get<Enemy>().spawnerRef.Del<HasActiveEnemy>();
                            GameObject.Destroy(enemy.Get<Enemy>().avatar.gameObject);
                            enemy.Destroy();
                        }
                    }
                    break;
            }
        }
    }
}