using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectScarlet
{
    public class UIController : MonoBehaviour
    {

        [SerializeField] private GameObject _player;
        [SerializeField] private Health _health;
        [SerializeField] private Image _healthOrb;
        [SerializeField] private Experience _experience;
        [SerializeField] private Image _experienceBar;
        [SerializeField] private Text _levelText;

        private void Awake()
        {

        }
        
        void Update()
        {

            if(_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player");
                _health = _player.GetComponent<Health>();
                _experience = _player.GetComponent<Experience>();                

                _health.OnDamage += HandleDamage;
                _health.OnHeal += HandleHeal;

                _experience.IncreaseExperience += HandleExperienceGain;
                _experience.OnLevelUp += HandleLevelUp;
            }
        }

        private void HandleLevelUp()
        {
            // update _levelText
            _levelText.text = _experience.GetCurrentLevel().ToString();
        }

        private void HandleExperienceGain()
        {
            _experienceBar.fillAmount = _experience.GetPercentage();
        }

        private void HandleHeal()
        {
            _healthOrb.fillAmount = _health.GetPercentage();
        }

        private void HandleDamage()
        {
            _healthOrb.fillAmount = _health.GetPercentage();
        }
    }
}