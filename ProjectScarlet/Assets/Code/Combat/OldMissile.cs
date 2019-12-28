using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class OldMissile : MonoBehaviour
    {
        [SerializeField] private float _missileSpeed = 5f;
        [SerializeField] private float _damage = 10f;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _target;

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }

        private void Update()
        {
            Vector3 middleOfTarget = new Vector3(_target.position.x, _target.GetComponent<Collider>().bounds.center.y, _target.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, middleOfTarget, _missileSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter(Collider other)
        {
            Health targetHealth = other.GetComponent<Health>();

            if (targetHealth != null)
            {
                targetHealth.ModifyHealth(-_damage);
                Destroy(this.gameObject);
            }
        }
    }
}