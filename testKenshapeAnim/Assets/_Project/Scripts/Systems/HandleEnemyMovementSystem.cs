using Leopotam.Ecs;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.AI;

namespace BombGame
{
    internal class HandleEnemyMovementSystem : IEcsRunSystem
    {
        private readonly GameData _data = null;
        
        private readonly EcsFilter<Enemy> _enemyFilter = null;
        private readonly EcsFilter<Control> _controlFilter = null;

        public void Run()
        {
            if (_enemyFilter.IsEmpty()) return;

            foreach (var index in _enemyFilter)
            {
                ref var enemy = ref _enemyFilter.GetEntity(index);
                ref var enemyView = ref enemy.Get<Enemy>().View;

                enemyView.transform.position += new Vector3(0, -7.5f, 0) * Time.deltaTime;

                if (!(enemyView.transform.position.y < 0)) continue;

                enemyView.GetComponent<NavMeshAgent>().enabled = true;
                enemyView.GetComponent<EnemyScript>().SetLanded();

                if (enemyView.transform.position.z < -8)
                {
                    Object.Destroy(enemyView);
                    enemy.Destroy();

                    if (_data.GameState == State.Gameplay)
                    {
                        _controlFilter.GetEntity(0).Get<Lose>();
                    }
                }
            }
        }
    }
}