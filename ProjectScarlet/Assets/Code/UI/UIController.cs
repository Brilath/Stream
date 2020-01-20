using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        [SerializeField] private DeathScreen _deathScreen;
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
            if (_player == null)
                initialze = false;

            if(!initialze)
                Initialize();

            if(Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadSceneAsync(0);
            }

        }

        private void SetupAbiltyBar()
        {
            int count = _abilityUI.transform.childCount;
            for(int i = 0; i < _abilityUI.transform.childCount; i++)
            {
                Destroy(_abilityUI.GetChild(i).gameObject);
            }

            foreach(Ability  ability in _abilityProcessor.GetAbilities())
            {
                var abilityIcon = Instantiate(_abilityUIPrefab, _abilityUI);
                abilityIcon.SetAbility(ability, _abilityProcessor);
            }
        }

        private void HandleLevelUp()
        {
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

        private void HandleDeath()
        {
            float respawnTime = _health.gameObject.GetComponent<Respawn>().RespawnCooldown;
            _deathScreen.gameObject.SetActive(true);            
            _deathScreen.CountDown(respawnTime);            
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
                _health.OnDeath += HandleDeath;

                _experience.IncreaseExperience += HandleExperienceGain;
                _experience.OnLevelUp += HandleLevelUp;

                SetupAbiltyBar();
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}