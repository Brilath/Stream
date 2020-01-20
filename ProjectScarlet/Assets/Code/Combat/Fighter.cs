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
        [SerializeField] private float _nextAttack;
        [SerializeField] private float _modifier;

        public Weapon CurrentWeapon { get { return _currentWeapon; } private set { _currentWeapon = value; } }
        public GameObject SpawnedWeapon { get { return _spawnedWeapon; } private set { _spawnedWeapon = value; } }
        public float NextAttack { get { return _nextAttack;  } set { _nextAttack = value; } }
        public Transform AttackPoint { get { return _attackPoint; } private set { _attackPoint = value; } }
        public float Modifier { get { return _modifier; } set { _modifier = value; } }

        private void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            Modifier = 1;
        }

        void Start()
        {
            if(CurrentWeapon != null)
            {
                SpawnedWeapon = CurrentWeapon.Spawn(_rightHand, _leftHand, _animator);
                AttackPoint = SpawnedWeapon.GetComponentInChildren<Transform>();
            }

            NextAttack = 0f;
        }

        public bool CanAttack()
        {
            return Time.time > NextAttack;
        }

        private void OnDrawGizmos()
        {
            if(AttackPoint != null)
                Gizmos.DrawWireSphere(AttackPoint.position, CurrentWeapon.AttackRange);
        }
    }
}