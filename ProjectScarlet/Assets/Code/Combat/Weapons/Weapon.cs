using UnityEngine;

namespace ProjectScarlet
{
    public abstract class Weapon : ScriptableObject
    {

        [SerializeField] private float _attackRange;
        [SerializeField] private float _attackSpeed;
        [SerializeField] private float _nextAttack;
        [SerializeField] private string _weaponName;
        [SerializeField] private WeaponHand _weaponHand;
        [SerializeField] private WeaponType _weaponType;
        [SerializeField] private GameObject _weaponPrefab;
        [SerializeField] private AnimatorOverrideController _animatorController;
        [SerializeField] private LayerMask _attackLayer;

        public float AttackRange { get { return _attackRange; } set { _attackRange = value; } }
        public float AttackSpeed { get { return _attackSpeed; } set { _attackSpeed = value; } }
        public float NextAttack { get { return _nextAttack; } set { _nextAttack = value; } }
        public string WeaponName { get { return _weaponName; } set { _weaponName = value; } }
        public WeaponHand Hand { get { return _weaponHand; } set { _weaponHand = value; } }
        public WeaponType Type { get { return _weaponType; } set { _weaponType = value; } }
        public GameObject WeaponPrefab { get { return _weaponPrefab; } set { _weaponPrefab = value; } }
        public AnimatorOverrideController WeaponAnimator { get { return _animatorController; } }
        public LayerMask AttackLayer { get { return _attackLayer; } set { _attackLayer = value; } }

        public Weapon()
        {
            AttackRange = 2f;
            AttackSpeed = 1f;
            WeaponName = "New Weapon";
            NextAttack = 0f;
        }

        public abstract void Attack(Transform attackPoint);

        public bool CanAttack()
        {
            return Time.time > NextAttack;
        }

        public GameObject Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            GameObject spawnedWeapon = null;

            if(WeaponPrefab != null)
            {
                Transform equipHand = GetWeaponHand(rightHand, leftHand);
                spawnedWeapon = Instantiate(WeaponPrefab, equipHand);
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if(WeaponAnimator != null)
            {
                animator.runtimeAnimatorController = WeaponAnimator;
            }
            else if(overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            return spawnedWeapon;
        }

        private Transform GetWeaponHand(Transform rightHand, Transform leftHand)
        {
            Transform handTransform = null;

            if (Hand == WeaponHand.Right)
            {
                handTransform = rightHand;
            }
            else if (Hand == WeaponHand.Left)
            {
                handTransform = leftHand;
            }            

            return handTransform;
        }

        private Transform GetWeaponHand(Transform rightHand, Transform leftHand, Transform twoHand)
        {
            Transform handTransform = null;

            if (Hand == WeaponHand.Right)
            {
                handTransform = rightHand;
            }
            else if (Hand == WeaponHand.Left)
            {
                handTransform = leftHand;
            }
            else
            {
                handTransform = twoHand;
            }

            return handTransform;
        }


    }
}