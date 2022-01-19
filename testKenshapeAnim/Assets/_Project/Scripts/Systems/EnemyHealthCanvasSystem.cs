using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class EnemyHealthCanvasSystem : IEcsRunSystem
    {
        private readonly GameData _data = null;

        private readonly EcsFilter<Health> _healthFilter = null;
        
        public void Run()
        {
            if (_data.GameState != State.Gameplay) return;

            foreach (var index in _healthFilter)
            {
                ref var entity = ref _healthFilter.GetEntity(index);
                var canvas = entity.Get<Enemy>().View.GetComponentInChildren<Canvas>();
                
                canvas.transform.forward = Vector3.forward;
            }
        }
    }
}