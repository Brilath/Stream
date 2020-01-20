using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Movement Modifier Ability", fileName = "Movement Modifier Ability")]
    public class MovementModifierAbility : ModifierAbility
    {
        [SerializeField] private bool _buff;

        public MovementModifierAbility()
        {
            Type = AbilityType.Utility;
        }

        public override void ProcessAbility(GameObject unit)
        {
            var motor = unit.GetComponent<IMotor>();
            if (motor == null) return;

            Collider[] targets = Physics.OverlapSphere(motor.transform.position,
                                                _range);

            foreach (Collider target in targets)
            {
                var tarMotor = target.gameObject.GetComponent<IMotor>();
                if (tarMotor != null) 
                {
                    if(target.gameObject.layer == unit.layer && _buff)
                    {
                        _unitOrginalStates.Add(target.gameObject, tarMotor.Modifier);
                    }
                    else if(!_buff && target.gameObject.layer != unit.layer)
                    {
                        _unitOrginalStates.Add(target.gameObject, tarMotor.Modifier);
                    }
                }
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
                unit.Key.GetComponent<IMotor>().Modifier = _modifier;
            }

            await Task.Delay(TimeSpan.FromSeconds(_modifiedTime));

            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.GetComponent<IMotor>().Modifier = unit.Value;
            }

            _unitOrginalStates.Clear();
        }
    }
}