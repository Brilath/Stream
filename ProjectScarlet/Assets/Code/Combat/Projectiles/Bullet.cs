using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Bullet : Projectile
    {
        [SerializeField] private float _lifeSpan = 3f;

        public override void Launch(Transform launchPostition)
        {
            GetComponent<Rigidbody>().AddForce(transform.forward * 1000);
            Destroy(gameObject, _lifeSpan);
        }
    }
}