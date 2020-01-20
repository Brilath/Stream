using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Melee Ability", fileName = "Melee Combat Ability")]
    public class MeleeCombatAbility : CombatAbility
    {
        [SerializeField] protected float _range = 10;
        [SerializeField] protected float _damage = 75;
        public float Range { get { return _range; } protected set { _range = value; } }
        public float Damage { get { return _damage; } protected set { _damage = value; } }

        public MeleeCombatAbility()
        {
            Type = AbilityType.Combat;
        }

        public override async void ProcessAbility(GameObject unit)
        {
            
            var figter = unit.gameObject.GetComponent<Fighter>();
            Animator anim = unit.GetComponentInChildren<Animator>();
            
            await Task.Delay(TimeSpan.FromSeconds(CastTime));

            Collider[] targets = Physics.OverlapSphere(unit.transform.position, Range);

            if (anim != null)
                anim.Play(_clip.name);

            foreach (Collider target in targets)
            {
                if (unit.layer != target.gameObject.layer)
                {
                    var health = target.GetComponent<Health>();
                    if (health != null)
                    {
                        health.ModifyHealth(-Damage * figter.Modifier);
                    }
                }
            }
        }
    }
}