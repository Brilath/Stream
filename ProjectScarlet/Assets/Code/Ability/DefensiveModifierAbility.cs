using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Modifier Ability", fileName = "Defensive Modifier Ability")]
    public class DefensiveModifierAbility : ModifierAbility
    {

        public DefensiveModifierAbility()
        {
            Type = AbilityType.Defensive;
        }

        public override void ProcessAbility(GameObject unit)
        {
            var health = unit.GetComponent<Health>();
            _orginalModifier = health.GetModifier();

            if(health != null)
                BuffUnit(health);      
        }

        protected async void BuffUnit(Health health)
        {
            health.SetModifier(_modifier);

            await Task.Delay(_modifiedTime * 1000);

            health.SetModifier(_orginalModifier);
        }
    }
}