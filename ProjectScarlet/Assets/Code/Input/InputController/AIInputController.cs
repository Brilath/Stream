using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public class AIInputController : MonoBehaviour
    {

        [SerializeField] private NPCMotor _motor;
        [SerializeField] private Transform _transform;
        [SerializeField] private AttackPath _path;

        [SerializeField] private int _currentWaypoint;
        [SerializeField] private float _waypointLeeway = 1f;
        [SerializeField] private LayerMask _attackLayer;
        [SerializeField] private Fighter _fighter;
        [SerializeField] private Transform _target;
        [SerializeField] private float _chaseRange = 5f;
        [SerializeField] private Animator _animator;

        private bool _isAttacking;
        private bool _isNavigating;

        private void Awake()
        {
            SetupInput();
        }

        private void SetupInput()
        {
            _motor = GetComponent<NPCMotor>();
            _transform = GetComponent<Transform>();
            if (_path == null)
            {
                _path = new AttackPath();
            }

            _fighter = GetComponent<Fighter>();
            _animator = GetComponentInChildren<Animator>();

            _isNavigating = true;
            _currentWaypoint = 0;
        }

        private void OnEnable() 
        {
            _currentWaypoint = 0;
            _target = null;
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            //Vector3 destination = new Vector3(_transform.position.x + 1, _transform.position.y, _transform.position.z);
            if (_target == null)
                WaypointBehaviour();
            else
                AttackBehaviour();

        }

        void FixedUpdate()
        {
            RefreshTarget();
        }

        private void AttackBehaviour()
        {            
            Vector3 _lookAt = _target.position - _transform.position;
            _motor.RotateTowards(_lookAt);

            float distance = Vector3.Distance(_transform.position, _target.position);

            if (distance <= _fighter.CurrentWeapon.AttackRange 
                && _fighter.CanAttack())
            {
                _animator.SetTrigger("attack");

                StartCoroutine(AttackDelay(_fighter.CurrentWeapon));
            }
            else if(distance > _fighter.CurrentWeapon.AttackRange)
            {
                _motor.MoveTo(_target.position);
            }
        }

        private IEnumerator AttackDelay(Weapon weapon)
        {
            _motor.CanMove = false;
            _motor.Stop();

            GameObject spawnedWeapon = _fighter.SpawnedWeapon;

            Animator weaponAnimator = spawnedWeapon.GetComponent<Animator>();
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("attack");
            }

            Transform attackPoint = spawnedWeapon.GetComponentInChildren<Transform>();
            _fighter.NextAttack = Time.time + weapon.AttackSpeed;

            yield return new WaitForSeconds(weapon.DelayTime);

            weapon.Attack(attackPoint);

            _motor.CanMove = true;
        }

        private void WaypointBehaviour()
        {
            if (AtWaypoint())
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
            }

            Vector3 destination = _path.GetWayPoint(_currentWaypoint);

            if (AtWaypoint())
            {
                _motor.Stop();
            }
            else
            {
                _motor.MoveTo(destination);
            }
        }

        private void RefreshTarget()
        {
            float targetRange = _fighter.CurrentWeapon.AttackRange;
            if (_chaseRange > _fighter.CurrentWeapon.AttackRange)
                targetRange = _chaseRange;

            Collider[] enemies = Physics.OverlapSphere(_transform.position,
                    targetRange, _attackLayer);

            if (enemies.Length > 0)
                _target = enemies[0].transform;
            else
                _target = null;             
        }

        private void OnTriggerEnter(Collider other) 
        {
            if (other.gameObject.tag == "Zombie")
            {
                _target = other.gameObject.transform;
            }
        }

        private float Sum(float num1, float num2)
        {
            return num1 + num2;
        }

        private bool  AtWaypoint()
        {
            bool _atWaypoint = false;

            if (_path == null) return true;

            float distanceToWaypoint = Vector3.Distance(_transform.position, _path.GetWayPoint(_currentWaypoint));

            if(distanceToWaypoint <= _waypointLeeway)
            {
                _atWaypoint = true;
            }

            return _atWaypoint;
        }

        public void SetAttackPath(AttackPath attackPath)
        {
            _path = attackPath;
        }
    }
}