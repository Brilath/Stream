﻿using UnityEngine;

namespace ProjectScarlet
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected Transform _target;
        [SerializeField] protected float _projectileSpeed = 25f;
        [SerializeField] protected float _damage = 10f;
        [SerializeField] protected Transform _transform;
        [SerializeField] protected int[] _attackLayers;
        public Transform Target { get { return _target; } set { _target = value; } }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }


        public abstract void Launch(Transform launchPostition, float multiplier = 1);

        private void OnTriggerEnter(Collider other)
        {

            Health targetHealth = other.GetComponent<Health>();
            int layer = other.gameObject.layer;
            
            if(!targetHealth.enabled) Destroy(this.gameObject);

            if (targetHealth != null &&
                targetHealth.enabled &&               
                CanAttack(layer))
            {
                targetHealth.ModifyHealth(-_damage);
                Destroy(this.gameObject, 2);
            }
        }

        private bool CanAttack(int layer)
        {
            bool canAttack = false;

            for (int i = 0; i < _attackLayers.Length; i++)
            {
                if (_attackLayers[i] == layer)
                {
                    canAttack = true;
                    break;
                }
            }

            return canAttack;
        }

    }
}