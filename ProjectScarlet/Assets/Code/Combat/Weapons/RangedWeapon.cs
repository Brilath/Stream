﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Weapons/Ranged Weapon", fileName = "Ranged Weapon")]
    public class RangedWeapon : Weapon
    {

        [SerializeField] private Projectile _projectile;
        [SerializeField] private Animator _weaponAnim;        
        public Projectile Projectile_ { get { return _projectile; } set { _projectile = value; } }
         
        public RangedWeapon()
        {
            Type = WeaponType.Ranged;
            DelayTime = 1.5f;
        }

        public override void Attack(Transform attackPoint)
        {
            if(attackPoint != null)
            {
                var projectile = Instantiate(_projectile, attackPoint.position, attackPoint.rotation);
                projectile.Launch(attackPoint);
            }
        }
    }
}