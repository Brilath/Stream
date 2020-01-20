using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Defensive Modifier Ability", fileName = "Defensive Modifier Ability")]
    public class DefensiveModifierAbility : ModifierAbility
    {
        public DefensiveModifierAbility()
        {
            Type = AbilityType.Defensive;
        }

        public override void ProcessAbility(GameObject unit)
        {
            var health = unit.GetComponent<Health>();
            if (health == null) return;

            Collider[] targets = Physics.OverlapSphere(health.transform.position,
                                                _range);

            foreach (Collider target in targets)
            {
                var tarHealth = target.gameObject.GetComponent<Health>();
                if (tarHealth != null && 
                    target.gameObject.layer == unit.layer)
                    _unitOrginalStates.Add(target.gameObject, tarHealth.Modifier);
            }

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);

            BuffUnits();
        }

        protected async void BuffUnits()
        {
            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.GetComponent<Health>().Modifier = _modifier;
            }

            await Task.Delay(_modifiedTime * 1000);

            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.GetComponent<Health>().Modifier = unit.Value;
            }
            _unitOrginalStates.Clear();
        }
    }
}