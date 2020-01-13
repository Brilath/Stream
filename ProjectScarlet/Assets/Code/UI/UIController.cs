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
        // Abilities
        [SerializeField] private Transform _abilityUI;
        [SerializeField] private AbilityIcon _abilityUIPrefab;
        [SerializeField] private CharacterAbilityProcessor _abilityProcessor;

        private bool initialze;

        private void Awake()
        {

        }

        private void Start() 
        {

        }
        
        void Update()
        {

            if(!initialze)
                Initialize();

        }

        private void SetupAbiltyBar()
        {
            foreach(Ability  ability in _abilityProcessor.GetAbilities())
            {
                var abilityIcon = Instantiate(_abilityUIPrefab, _abilityUI);
                abilityIcon.SetAbility(ability, _abilityProcessor);
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

        private void Initialize()
        {
            if (_player == null)
                _player = GameObject.FindGameObjectWithTag("Player");

            if (_player != null)
            {
                initialze = true;

                _health = _player.GetComponent<Health>();
                _experience = _player.GetComponent<Experience>();
                _abilityProcessor = _player.GetComponent<CharacterAbilityProcessor>();

                _health.OnDamage += HandleDamage;
                _health.OnHeal += HandleHeal;

                _experience.IncreaseExperience += HandleExperienceGain;
                _experience.OnLevelUp += HandleLevelUp;

                SetupAbiltyBar();
            }
        }
    }
}