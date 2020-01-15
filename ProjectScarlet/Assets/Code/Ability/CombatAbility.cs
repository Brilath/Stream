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
        [SerializeField] private int castTime = 20;

        public CombatAbility()
        {
            Type = AbilityType.Combat;
        }

        public override async void ProcessAbility(GameObject unit)
        {
            Collider[] targets = Physics.OverlapSphere(unit.transform.position, range);

            Animator anim = unit.GetComponentInChildren<Animator>();        

            var currentOverrideController = anim.runtimeAnimatorController as AnimatorOverrideController;

            if (overrideController != null)
            {
                anim.runtimeAnimatorController = overrideController;
            }
            else if (currentOverrideController != null)
            {
                anim.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }            

            anim.SetTrigger("ability");            

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

            var results = await WaitSeconds(castTime);

            anim.runtimeAnimatorController = currentOverrideController;         
        }

        private async Task<int> WaitSeconds(int seconds)
        {
            await Task.Delay(seconds * 1000);

            return 0;
        }
    }
}