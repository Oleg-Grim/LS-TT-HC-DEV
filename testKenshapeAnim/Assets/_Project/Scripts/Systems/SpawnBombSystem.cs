using Leopotam.Ecs;
using UnityEngine;

namespace BombGame
{
    internal class SpawnBombSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;

        private readonly GameData _data = null;
        private readonly Configuration _config = null;
        private readonly References _refs = null;

        public void Run()
        {
            if (_data.GameState != State.Gameplay) return;
            if(_data.GameplayTimer > 29) return;
            
            if (_data.BombTimer > 0)
            {
                _data.BombTimer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    var ray = _refs.Cam.ScreenPointToRay(Input.mousePosition);

                    if (Physics.Raycast(ray, out var hit))
                    {
                        var targetPos = new Vector3(hit.point.x, 10, hit.point.z);

                        var bombEntity = _world.NewEntity();
                        ref var bomb = ref bombEntity.Get<Bomb>();

                        bomb.View = Object.Instantiate(_refs.BombPrefab, targetPos, Quaternion.identity);
                        _data.BombTimer = _config.BombTimerDefault;
                    }
                }
            }
        }
    }
}