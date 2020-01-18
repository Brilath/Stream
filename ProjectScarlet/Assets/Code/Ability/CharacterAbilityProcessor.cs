using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectScarlet
{
    public class CharacterAbilityProcessor : MonoBehaviour
    {
        [SerializeField] private List<Ability> _abilities = new List<Ability>();
        [SerializeField] private Dictionary<int, bool> _abilityAvailability = new Dictionary<int, bool>();
        [SerializeField] private bool _canCast = true;
        [SerializeField] private float _abilityCooldown = 1.5f;
        [SerializeField] private float _abilityCountDown;

        public event Action<Ability> OnAbilityUsed = delegate { };
        public event Action<CharacterAbilityProcessor> OnGlobalCooldown = delegate { };

        public float GlobalCooldown { get { return _abilityCooldown; } }
        public float AbilityCountDown { get { return _abilityCountDown; } private set { _abilityCountDown = value; } }
        
        private void Start()
        {
            for(int i = 0; i < _abilities.Count; i++)
            {
                _abilityAvailability.Add(i, true);
            }
        }

        private void OnEnable() 
        {
            for(int i = 0; i < _abilityAvailability.Count; i++)
            {
                _abilityAvailability[i] = true;
            }    
        }

        private void Update() 
        {
  
        }        

        public Ability GetAbility(int num)
        {
            Ability charAbility = null;

            if(_abilityAvailability[num] && _canCast)
            {               
                charAbility = _abilities[num];                                           
                UsedAbility(_abilities[num], num);
                OnAbilityUsed(charAbility);
                OnGlobalCooldown(this);
            }
            return charAbility;                    
        }

        public List<Ability> GetAbilities()
        {
            return _abilities;
        }

        private void UsedAbility(Ability ability, int abilityNumber)
        {
            _abilityAvailability[abilityNumber] = false;

            StartCoroutine(AbilityOnCooldown(ability, abilityNumber));
            StartCoroutine(StartAbilityCooldown());
        }

        private IEnumerator AbilityOnCooldown(Ability ability, int abilityNumber)
        {
            ability.CountDown = ability.Cooldown;

            while(ability.CountDown > 0)
            {
                ability.CountDown -= Time.deltaTime;
                yield return null;
            }            

            _abilityAvailability[abilityNumber] = true;
        }

        private IEnumerator StartAbilityCooldown()
        {
            _canCast = false;
            AbilityCountDown = _abilityCooldown;
            while(AbilityCountDown > 0)
            {
                AbilityCountDown -= Time.deltaTime;
                yield return null;
            }            
            _canCast = true;
        }
    }
}