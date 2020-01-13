using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectScarlet
{
    public class AbilityIcon : MonoBehaviour
    {
        
        [SerializeField] private Image _icon;
        [SerializeField] private Image _cooldownImage;
        [SerializeField] private Ability _ability;
        [SerializeField] private CharacterAbilityProcessor _processor;        

        public void SetAbility(Ability ability, CharacterAbilityProcessor processor)
        {
            _ability = ability;
            _processor = processor;

            _icon.sprite = ability.Icon;

            _processor.OnAbilityUsed += HandleAbilityUsed;            
        }

        private void HandleAbilityUsed(Ability ability)
        {
            if(_ability == ability)
            {
                StartCoroutine(AbilityOnCooldown());
            }
        }

        private IEnumerator AbilityOnCooldown()
        {
            float countDown = _ability.Cooldown;
            while (countDown > 0)
            {
                countDown -= Time.deltaTime;
                _cooldownImage.fillAmount = _ability.CountDown / _ability.Cooldown;
                yield return null;
            }
        }
    }
}