using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Combat Ability", fileName = "Combat Ability")]
    public class CombatAbility : Ability
    {
        [SerializeField] private float range = 10;
        [SerializeField] private float damage = 75;

        public CombatAbility()
        {
            Type = AbilityType.Combat;
        }

        public override void ProcessAbility(GameObject unit)
        {
            Collider[] targets = Physics.OverlapSphere(unit.transform.position, range);

            Animator anim = unit.GetComponentInChildren<Animator>();    

            if (anim != null)
                anim.Play(_clip.name);           

            foreach(Collider target in targets)
            {
                if (unit.layer != target.gameObject.layer)
                {
                    var health = target.GetComponent<Health>();
                    if(health != null)
                    {
                        health.ModifyHealth(-damage);
                    }
                }
            }       
        }
    }
}