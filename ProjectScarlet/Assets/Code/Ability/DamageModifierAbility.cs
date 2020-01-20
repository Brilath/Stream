using System.Collections;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Damage Modifier Ability", fileName = "Damage Modifier Ability")]
    public class DamageModifierAbility : ModifierAbility
    {
        public DamageModifierAbility()
        {
            Type = AbilityType.Combat;
        }

        public override void ProcessAbility(GameObject unit)
        {
            var fighter = unit.GetComponent<Fighter>();
            if (fighter == null) return;

            Collider[] targets = Physics.OverlapSphere(fighter.transform.position, 
                                                _range);

            foreach(Collider target in targets)
            {
                var tarFighter = target.gameObject.GetComponent<Fighter>();
                if(tarFighter != null && 
                    target.gameObject.layer == unit.layer)
                    _unitOrginalStates.Add(target.gameObject, tarFighter.Modifier);
            }

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);
            
            BuffUnits();
        }

        protected async void BuffUnits()
        {
            foreach(KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.GetComponent<Fighter>().Modifier = _modifier;
            }

            await Task.Delay(TimeSpan.FromSeconds(_modifiedTime));

            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.GetComponent<Fighter>().Modifier = unit.Value;
            }

            _unitOrginalStates.Clear();
        }
    }
}