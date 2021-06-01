using Leopotam.Ecs;
using UnityEngine;

namespace Client
{
    internal class BombDropSystem : IEcsRunSystem
    {
        private EcsFilter<Bomb, ActiveBomb> _filter;

        public void Run()
        {
            if (!_filter.IsEmpty())
            {
                foreach (var index in _filter)
                {
                    var bomb = _filter.GetEntity(index);
                    var _avatar = bomb.Get<Bomb>().avatar;

                    bomb.Get<Bomb>().position -= new Vector3(0,1,0) * Time.deltaTime * bomb.Get<Bomb>().speed;

                    _avatar.transform.position = bomb.Get<Bomb>().position;

                    if(bomb.Get<Bomb>().position.y < 0)
                    {
                        bomb.Del<ActiveBomb>();
                        bomb.Get<Explode>(); 
                    }
                }
            }
        }
    }
}