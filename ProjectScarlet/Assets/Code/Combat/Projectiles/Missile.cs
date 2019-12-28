﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Missile : Projectile
    {
        
        [SerializeField] private Collider _targetCollider;
        


        private void Start()
        {
            if(Target != null )
            {
                SetTargetCollider();
            }
        }

        private void SetTargetCollider()
        {
            _targetCollider = Target.GetComponent<Collider>();
        }

        public override void Launch(Transform launchPostition)
        {
            //GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }

        public void Launch(Transform launchPostition, Transform target)
        {
            Target = target;
            //GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
        }

        private void Update()
        {
            if (Target == null) return;
            if (_targetCollider == null) SetTargetCollider();

            Vector3 middleOfTarget = new Vector3(Target.position.x, _targetCollider.bounds.center.y, Target.position.z);
            _transform.position = Vector3.MoveTowards(_transform.position, middleOfTarget, _projectileSpeed * Time.deltaTime);
        }
    }
}