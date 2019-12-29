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
            if(_target == null)
                RefreshTarget();
        }

        private void AttackBehaviour()
        {
            _motor.MoveTo(_target.position);
            Vector3 _lookAt = _target.position - _transform.position;
            _motor.RotateTowards(_lookAt);

            float distance = Vector3.Distance(_transform.position, _target.position);

            if (distance <= _fighter.CurrentWeapon.AttackRange 
                && _fighter.CanAttack())
            {
                _animator.SetTrigger("attack");
                Transform attackPoint = _fighter.SpawnedWeapon.GetComponentInChildren<Transform>();
                _fighter.CurrentWeapon.Attack(attackPoint);
                _fighter.NextAttack = Time.time + _fighter.CurrentWeapon.AttackSpeed;
            }
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
            Collider[] enemies = Physics.OverlapSphere(_transform.position, _chaseRange, _attackLayer);
            Debug.Log($"Targets in range: {enemies.Length}");
            if (enemies.Length > 0)
                _target = enemies[0].transform;
            else
                _target = null;             
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