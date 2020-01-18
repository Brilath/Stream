using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Bullet : Projectile
    {
        [SerializeField] private float _lifeSpan = 3f;

        public override void Launch(Transform launchPostition, float multiplier = 1)
        {
            _damage *= multiplier;
            GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            Destroy(gameObject, _lifeSpan);
        }
    }
}