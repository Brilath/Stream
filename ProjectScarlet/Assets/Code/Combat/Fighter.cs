using UnityEngine;

namespace ProjectScarlet
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _bothHand;
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private GameObject _spawnedWeapon;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            if(_currentWeapon != null)
            {
                _spawnedWeapon = _currentWeapon.Spawn(_rightHand, _leftHand, _animator);
            }
        }

    }
}