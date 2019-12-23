using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Weapons/Melee Weapon", fileName = "Melee Weapon")]
    public class MeleeWeapon : Weapon
    {
        [SerializeField] private float _weaponDamage;
        public float WeaponDamage { get { return _weaponDamage; } private set { _weaponDamage = value; } }

        public MeleeWeapon()
        {
            WeaponDamage = 15;
            Type = WeaponType.Melee;
        }
    }
}