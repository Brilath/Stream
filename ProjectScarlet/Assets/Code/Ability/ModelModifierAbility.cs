using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using UnityEngine;
namespace ProjectScarlet
{
    [CreateAssetMenu(menuName="Ability/Model Modifier Ability", fileName = "Model Modifier Ability")]
    public class ModelModifierAbility : ModifierAbility
    {
        [SerializeField] private float _castTime;

        public override void ProcessAbility(GameObject unit)
        {
            var transform = unit.GetComponent<Transform>();
            if (transform == null) return;

            Collider[] targets = Physics.OverlapSphere(transform.transform.position,
                                                _range);

            foreach (Collider target in targets)
            {
                var tartransform = target.gameObject.GetComponent<Transform>();
                if (tartransform != null &&
                    target.gameObject.layer == unit.layer)
                    _unitOrginalStates.Add(target.gameObject, 0);
            }

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(_clip.name);

            BuffUnits();
        }

        protected async void BuffUnits()
        {
            await Task.Delay(TimeSpan.FromSeconds(_castTime));

            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.transform.GetChild(0).gameObject.SetActive(false);
            }

            await Task.Delay(TimeSpan.FromSeconds(_modifiedTime));

            foreach (KeyValuePair<GameObject, float> unit in _unitOrginalStates)
            {
                unit.Key.transform.GetChild(0).gameObject.SetActive(true);
            }

            _unitOrginalStates.Clear();
        }
    }
}