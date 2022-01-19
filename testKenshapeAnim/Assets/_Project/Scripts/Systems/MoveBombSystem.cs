using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class MoveBombSystem : IEcsRunSystem
    {
        private readonly Configuration _config = null;
        
        private readonly EcsFilter<Bomb> _bombFilter = null;
        
        public void Run()
        {
            if(_bombFilter.IsEmpty()) return;

            foreach (var index in _bombFilter)
            {
                ref var bomb = ref _bombFilter.Get1(index).View;
                
                bomb.transform.position += new Vector3(0, -_config.BombFallSpeed * Time.deltaTime, 0);
            }
        }
    }
}