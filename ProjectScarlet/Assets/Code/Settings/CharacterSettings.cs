using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Character/Settings", fileName = "CharacterSettings")]
    public class CharacterSettings : ScriptableObject
    {

        [SerializeField] private float _turnSpeed = 10f;
        [SerializeField] private float _baseSpeed = 3.25f;
        [SerializeField] private float _currentSpeed = 3.25f;
        [SerializeField] private float _maxSpeed = 3.25f;
        [SerializeField] private float _sprintMultiplier = 2f;
        [SerializeField] private float _moveToSpeed = 10f;
        [SerializeField] private float _rotateSpeed = 10f;
        [SerializeField] private float _jumpPower = 5f;
        [SerializeField] private float _jumpHeight = 3f;
        [SerializeField] private bool _canMove = true;
        [SerializeField] private Vector3 _moveDirection = Vector2.zero;
        [SerializeField] private float _verticalDirection = 0f;
        
        public float TurnSpeed { get { return _turnSpeed; } }
        public float BaseSpeed { get { return _baseSpeed; } }
        public float SprintMultiplier { get { return _sprintMultiplier; } }
        public float MoveToSpeed { get { return _moveToSpeed; } }
        public float RotationSpeed { get { return _rotateSpeed; } }
        public float JumpPower { get { return _jumpPower; } }
        public float JumpHeight { get { return _jumpHeight; } }

        public float CurrentSpeed { get { return _currentSpeed; } set { _currentSpeed = value; } }
        public float MaxSpeed { get { return _maxSpeed; } set { _maxSpeed = value; } }
        public bool CanMove { get { return _canMove; } set { _canMove = value; } }
        public Vector3 MoveDirection { get { return _moveDirection; } set { _moveDirection = value; } }
        public float VerticalDirection { get { return _verticalDirection; } set { _verticalDirection = value; } }
    }
}