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

        public override void Attack(Transform attackPoint, float modifier)
        {
            Collider[] enemies = Physics.OverlapSphere(attackPoint.position, 
                    AttackRange, AttackLayer);

            Health enemyHealth;

            Debug.Log("Weapon Attack");

            foreach (Collider enemy in enemies)
            {
                enemyHealth = enemy.GetComponent<Health>();

                if(enemyHealth != null)
                {
                    Debug.Log("Doing Damage");
                    enemyHealth.ModifyHealth(-WeaponDamage * modifier);
                }
            }
        }
    }
}