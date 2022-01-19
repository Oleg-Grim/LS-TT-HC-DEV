using UnityEngine;
using UnityEngine.AI;

namespace BombGame
{
    public class EnemyScript : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _agent;
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _agent = GetComponent<NavMeshAgent>();
        }

        public void SwitchLandedToRunning()
        {
            _animator.SetBool("isRunning", true);
            _agent.SetDestination(new Vector3(Random.Range(-4,5), 0, -10));
        }

        public void SetLanded()
        {
            _animator.SetBool("hasLanded", true);
        }
    }
}
