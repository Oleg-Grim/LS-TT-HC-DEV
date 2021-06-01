using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Client
{
    internal class EnemySystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, ActiveEnemy> _filter;
        private Configuration _config;

        public void Run()
        {
            if (!_filter.IsEmpty()) 
            {
                var xMin = _config.worldMinBounds.x;
                var xMax = _config.worldMaxBounds.x;
                var zMax = _config.loseLine - 1.5f;
                var zMin = _config.worldMinBounds.z;

                if (!_filter.IsEmpty())
                {
                    foreach (var index in _filter)
                    {
                        var tmp = _filter.GetEntity(index);
                        var target = new Vector3(Random.Range(xMin, xMax), 0.35f, Random.Range(zMin, zMax));
                        
                        var _avatar = tmp.Get<Enemy>().avatar;
                        
                        if(tmp.Get<Enemy>().target == Vector3.zero)
                        {
                            tmp.Get<Enemy>().target = target;
                        }

                        _avatar.GetComponent<NavMeshAgent>().SetDestination(tmp.Get<Enemy>().target);

                        if(_avatar.transform.position.z < _config.loseLine)
                        {
                            _filter.GetEntity(index).Get<HasReachedEnd>();
                        }

                    }
                }
            }
        }
    }
}