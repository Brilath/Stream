using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName= "Ability/Ultimate Ability", fileName = "Ultimate Ability")]
    public class UltimateAbility : Ability
    {
        [SerializeField] private List<Ability> _abilities;
        
        public UltimateAbility()
        {            
            Type = AbilityType.Ultimate;
        }

        public override void ProcessAbility(GameObject unit)
        {
            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);

            foreach(Ability abilty in _abilities)
            {
                abilty.ProcessAbility(unit);
            }          
        }
    }
}