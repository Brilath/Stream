using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [RequireComponent(typeof(ICharacterInput))]
    public class CharacterMotor : MonoBehaviour
    {
        [SerializeField] private ICharacterInput _characterInput;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private CapsuleCollider _collider;
        [SerializeField] private CharacterSettings _settings;
        [SerializeField] private Transform _transform;
        [SerializeField] private bool isJumping;
        [SerializeField] private bool isSprinting;
        [SerializeField] private bool canMove;

        public bool CanMove { get { return canMove; } set { canMove = value; } }

        private void Awake()
        {
            _characterInput = GetComponent<ICharacterInput>();
            _rigidbody = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _transform = GetComponent<Transform>();
            CanMove = true;
        }

        void Start()
        {

        }

        private void OnEnable()
        {
            CanMove = true;
        }

        private void Update()
        {

            if (_settings.MoveDirection.magnitude > 0)
                _settings.CurrentSpeed = Mathf.MoveTowards(_settings.CurrentSpeed, _settings.MaxSpeed, ChangeMoveSpeed() / 2);
            else
                _settings.CurrentSpeed = Mathf.MoveTowards(_settings.CurrentSpeed, 0f, ChangeMoveSpeed());
        }

        private void FixedUpdate()
        {
            Vector3 velocity = _rigidbody.velocity;
            float rotationChangeSpeed = _settings.RotationSpeed * Time.deltaTime;

            if (CanMove)
            {
                velocity.x = Mathf.MoveTowards(velocity.x, _settings.MoveDirection.x, ChangeMoveSpeed());
                velocity.y = Mathf.MoveTowards(velocity.y, _settings.VerticalDirection, ChangeMoveSpeed());
                velocity.z = Mathf.MoveTowards(velocity.z, _settings.MoveDirection.z, ChangeMoveSpeed());

                _rigidbody.velocity = velocity;
            }

            if (_settings.MoveDirection.magnitude > 0.1)
            {
                _transform.rotation = Quaternion.Slerp(_transform.rotation,
                    Quaternion.LookRotation(_settings.MoveDirection), rotationChangeSpeed);
            }

            CheckVertical();
        }

        private float ChangeMoveSpeed()
        {
            return _settings.MoveToSpeed * Time.deltaTime * _settings.MaxSpeed;
        }

        private void CheckVertical()
        {
            if (_transform.position.y >= _settings.JumpHeight)
            {
                _settings.VerticalDirection = -_settings.JumpPower;
            }

            if (IsGrounded() && isJumping)
            {
                _settings.VerticalDirection = 0f;
                isJumping = false;
            }
        }

        private bool IsGrounded()
        {
            RaycastHit hit;
            bool _isGrounded = false;
            float maxRayDistance = _collider.bounds.extents.y + 0.00000001f;

            if (Physics.Raycast(_collider.bounds.center, -Vector3.up, out hit, maxRayDistance))
            {
                _isGrounded = true;
            }

            return _isGrounded;
        }

        public void Tick()
        {

        }

        public void Move()
        {

        }

        public void Stop()
        {
            _rigidbody.velocity = Vector3.zero;
        }

        public void ToggleSprint()
        {
            isSprinting = !isSprinting;

            if (isSprinting)
            {
                _settings.MaxSpeed = _settings.BaseSpeed * _settings.SprintMultiplier;
            }
            else
            {
                _settings.MaxSpeed = _settings.BaseSpeed;
            }
        }

        public void Jump()
        {
            if (!isJumping)
            {
                _settings.VerticalDirection = _settings.JumpPower;
                isJumping = true;
            }
        }
    }
}