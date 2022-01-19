using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class GameplayTimerSystem : IEcsRunSystem
    {
        private readonly GameData _data = null;

        private float _sillyFloatTimer;
        private bool _shouldRun = true;
        
        public void Run()
        {
            if (_data.GameState != State.Gameplay)
            {
                _shouldRun = true;
            }
            
            if(!_shouldRun) return;
            
            ref var time = ref _data.GameplayTimer;

            if(_data.GameState != State.Gameplay) return;

            if (time > 0)
            {
                if (_sillyFloatTimer < 1)
                {
                    _sillyFloatTimer += Time.deltaTime;
                }
                else
                {
                    time--;
                    _sillyFloatTimer = 0;
                    
                    CEvents.FireTimerChanged($"{time}");
                }
            }
            else
            {
                CEvents.FireTimerChanged($"BOMB THE REST!");
                _shouldRun = false;
            }
        }
    }
}