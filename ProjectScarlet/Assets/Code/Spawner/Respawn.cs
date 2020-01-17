using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Respawn : MonoBehaviour
    {

        [SerializeField] private GameObject _gameObject;
        [SerializeField] private Transform _transform;
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Quaternion _startRotation;
        [SerializeField] private float _respawnCooldown = 10f;

        [SerializeField] private Health _health;

        public float RespawnCooldown { get { return _respawnCooldown; } }

        private void Awake() 
        {
            _gameObject = this.gameObject;
            _transform = GetComponent<Transform>();
            _startPosition = _transform.position;
            _startRotation = _transform.rotation;

            _health = GetComponent<Health>();
            if(_health != null)
                 _health.OnDeath += RespawnUnit;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                ToggleComponents(false);
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                ToggleComponents(true);
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                RespawnUnit();
            }
        }

        public void RespawnUnit()
        {
            _transform.position = _startPosition;
            _transform.rotation = _startRotation;
            _gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ToggleComponents(false);

            StartCoroutine(StartRespawn());            
        }

        private void ToggleComponents(bool state)
        {
            _gameObject.GetComponent<PlayerInputController>().enabled = state;
            _gameObject.GetComponent<CharacterMotor>().enabled = state;
            _gameObject.GetComponent<Health>().enabled = state;
            transform.GetChild(0).gameObject.SetActive(state);        
        }

        private IEnumerator StartRespawn()
        {
            yield return new WaitForSeconds(10);

            ToggleComponents(true);
        }
    }
}