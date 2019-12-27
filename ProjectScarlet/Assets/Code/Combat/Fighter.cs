using UnityEngine;

namespace ProjectScarlet
{
    public class Fighter : MonoBehaviour
    {

        [SerializeField] private Transform _leftHand;
        [SerializeField] private Transform _rightHand;
        [SerializeField] private Transform _bothHand;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private Weapon _currentWeapon;
        [SerializeField] private GameObject _spawnedWeapon;
        [SerializeField] private Animator _animator;

        public Weapon CurrentWeapon { get { return _currentWeapon; } private set { _currentWeapon = value; } }
        public GameObject SpawnedWeapon { get { return _spawnedWeapon; } private set { _spawnedWeapon = value; } }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        void Start()
        {
            if(CurrentWeapon != null)
            {
                SpawnedWeapon = CurrentWeapon.Spawn(_rightHand, _leftHand, _animator);
                _attackPoint = SpawnedWeapon.GetComponentInChildren<Transform>();
            }
        }


        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(_attackPoint.position, CurrentWeapon.AttackRange);
        }
    }
}