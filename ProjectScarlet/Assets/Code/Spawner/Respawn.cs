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

        private void Awake() 
        {
            _gameObject = this.gameObject;
            _transform = GetComponent<Transform>();
            _startPosition = _transform.position;
            _startRotation = _transform.rotation;

            _health = GetComponent<Health>();
            // if(_health != null)
            //     _health.OnDeath += RespawnUnit;
        }

        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.I))
            {
                RespawnUnit();
            }
        }

        public void RespawnUnit()
        {
            StartCoroutine(StartRespawn());
        }

        private IEnumerator StartRespawn()
        {
            yield return new WaitForSeconds(_respawnCooldown);

            _transform.position = _startPosition;
            _transform.rotation = _startRotation;
            _health.SetupHealth();
        }
    }
}