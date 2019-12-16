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


        private void Awake() 
        {
            _motor = GetComponent<NPCMotor>();    
            _transform = GetComponent<Transform>();

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

            if(AtWaypoint())
            {
                _currentWaypoint = _path.GetNextWaypoint(_currentWaypoint);
            }

            Vector3 destination = _path.GetWayPoint(_currentWaypoint);

            if(AtWaypoint())
            {
                _motor.Stop();
            }
            else
            {
                _motor.MoveTo(destination);
            }            
        }

        private bool  AtWaypoint()
        {
            bool _atWaypoint = false;
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