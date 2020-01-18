using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Melee Ability", fileName = "Melee Combat Ability")]
    public class MeleeCombatAbility : CombatAbility
    {

        public MeleeCombatAbility()
        {
            Type = AbilityType.Combat;
        }

        public override void ProcessAbility(GameObject unit)
        {
            Collider[] targets = Physics.OverlapSphere(unit.transform.position, Range);

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);

            foreach (Collider target in targets)
            {
                if (unit.layer != target.gameObject.layer)
                {
                    var health = target.GetComponent<Health>();
                    if (health != null)
                    {
                        health.ModifyHealth(-Damage);
                    }
                }
            }
        }
    }
}