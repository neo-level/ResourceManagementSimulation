using UnityEngine;
using UnityEngine.AI;

namespace Helpers
{
    public class AnimatorHandler : MonoBehaviour
    {
        private Animator _mAnimator;
        private NavMeshAgent _mAgent;
        private static readonly int Speed = Animator.StringToHash("Speed");

        // Start is called before the first frame update
        private void Start()
        {
            _mAgent = GetComponentInParent<NavMeshAgent>();
            _mAnimator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (_mAgent != null && _mAnimator != null)
            {
                _mAnimator.SetFloat(Speed, _mAgent.velocity.magnitude / _mAgent.speed);
            }
        }
    }
}