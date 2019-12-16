using UnityEngine;

namespace ProjectScarlet
{
    public class AnimationController : MonoBehaviour
    {
        [SerializeField] private CharacterSettings _settings;
        [SerializeField] private Animator _animator;
        [SerializeField] private string FORWARD_SPEED = "forwardSpeed";

        private void Awake() 
        {
            _animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            _animator.SetFloat(FORWARD_SPEED, _settings.CurrentSpeed);
        }
    }
}