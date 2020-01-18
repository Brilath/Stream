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
        [SerializeField] private Image _globalcooldownImage;
        [SerializeField] private Text _abilityKeybind;
        [SerializeField] private Ability _ability;
        [SerializeField] private CharacterAbilityProcessor _processor;        

        public void SetAbility(Ability ability, CharacterAbilityProcessor processor)
        {
            _ability = ability;
            _processor = processor;

            _icon.sprite = ability.Icon;
            char bind = ability.KeyBind.ToString()[ability.KeyBind.ToString().Length-1];
            _abilityKeybind.text = bind.ToString();

            _processor.OnAbilityUsed += HandleAbilityUsed;
            _processor.OnGlobalCooldown += HandleGlobal;
            // To Do when we change the keybinds we update the ability ui                        
        }

        private void HandleAbilityUsed(Ability ability)
        {
            if(_ability == ability)
            {
                StartCoroutine(AbilityOnCooldown(ability));
            }
        }

        private void HandleGlobal(CharacterAbilityProcessor processor)
        {
            if(processor != null)
            {
                StartCoroutine(GlobalCooldown(processor));
            }
        }

        private IEnumerator AbilityOnCooldown(Ability ability)
        {
            //float countDown = _ability.Cooldown;
            while (ability.CountDown > 0)
            {
                //countDown -= Time.deltaTime;
                _cooldownImage.fillAmount = ability.CountDown / ability.Cooldown;
                yield return null;
            }
            _cooldownImage.fillAmount = 0;

            Vector3 orginalScale = Vector3.one;
            Vector3 maxScale = new Vector3(1.1f, 1.1f, 1.1f);
            float scaleTime = 1f;
            StartCoroutine(ScaleUpDown(orginalScale, maxScale, scaleTime));
        }

        private IEnumerator ScaleUpDown(Vector3 orginalScale, Vector3 targetScale, float seconds)
        {
            StartCoroutine(Scale(targetScale, seconds));

            yield return new WaitForSeconds(seconds);

            StartCoroutine(Scale(orginalScale, seconds));
        }

        private IEnumerator Scale(Vector3 targetScale, float seconds)
        {
            float scaleTime = seconds;
            float scaleSpeed = Vector3.Distance(transform.localScale, targetScale) / seconds;
            Debug.Log($"Scale Speed: {scaleSpeed}");
            while (scaleTime > 0)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.deltaTime / scaleSpeed);
                //Vector3.MoveTowards(transform.localScale, targetScale, Time.deltaTime / scaleSpeed);
                scaleTime -= Time.deltaTime;
            
                yield return null;
            }

            transform.localScale = targetScale;
        }

        private IEnumerator GlobalCooldown(CharacterAbilityProcessor processor)
        {
            while(processor.AbilityCountDown > 0)
            {
                _globalcooldownImage.fillAmount = processor.AbilityCountDown / processor.GlobalCooldown;
                 yield return null;
            }
            _globalcooldownImage.fillAmount = 0;
        }
    }
}