using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class ArcherController : MonoBehaviour
    {

        [SerializeField] private List<Transform> _targets = new List<Transform>();
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _newTarget;
        [SerializeField] private Transform _currentTarget;
        [SerializeField] private ArcherMotor _motor;
        [SerializeField] private float _attackRange = 10f;
        [SerializeField] private LayerMask _attackLayer;
        [SerializeField] private Fighter _fighter;
        [SerializeField] private Animator _animator;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
            _motor = GetComponent<ArcherMotor>();
            _fighter = GetComponent<Fighter>();
            _animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            SetTarget();

            _motor.SetTarget(_newTarget);

            if (_currentTarget != null)
            {
                RangedWeapon currentWeapon = _fighter.CurrentWeapon as RangedWeapon;
                if (_fighter.CanAttack())
                {
                    _animator.SetTrigger("attack");
                    transform.rotation *= Quaternion.Euler(0, 90, 0);
                    StartCoroutine(AttackDelay(currentWeapon.DelayTime, currentWeapon));
                }
            }
        }

        private void FixedUpdate()
        {
            RefreshTargets();
        }


        private IEnumerator AttackDelay(float seconds, RangedWeapon weapon)
        {
            GameObject spawnedWeapon = GetComponent<Fighter>().SpawnedWeapon;

            Animator weaponAnimator = spawnedWeapon.GetComponent<Animator>();
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("attack");
            }

            Transform attackPoint = spawnedWeapon.GetComponentInChildren<Transform>();
            _fighter.NextAttack = Time.time + weapon.AttackSpeed;

            yield return new WaitForSeconds(seconds);

            weapon.Projectile_.Target = _currentTarget;
            weapon.Attack(attackPoint);
            transform.rotation *= Quaternion.Euler(0, -90, 0);
        }

        private void SetTarget()
        {
            if (_currentTarget != null && _targets.Count > 0)
            {
                _newTarget = _currentTarget.position;
            }
            if (_currentTarget == null && _targets.Count > 0)
            {
                _currentTarget = _targets[0];
                _newTarget = _currentTarget.position;
            }
            if (_targets.Count == 0)
            {
                _currentTarget = null;
                _newTarget = Vector3.zero;
            }
        }

        private void RefreshTargets()
        {
            Collider[] enemies = Physics.OverlapSphere(_transform.position, _attackRange, _attackLayer);

            _targets.Clear();

            foreach (Collider enemy in enemies)
            {
                _targets.Add(enemy.transform);
            }
        }
    }
}