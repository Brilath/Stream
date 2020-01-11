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
            foreach(KeyValuePair<int, bool> availability in _abilityAvailability)
            {
                Debug.Log($"Index {availability.Key} Flag {availability.Value}");
            }    
        }        

        public Ability GetAbility(int num)
        {
            Ability charAbility = null;

            if(_abilityAvailability[num])
            {               
                charAbility = _abilities[num];
                UsedAbility(_abilities[num], num);                
            }
            return charAbility;                    
        }

        private void UsedAbility(Ability ability, int abilityNumber)
        {
            _abilityAvailability[abilityNumber] = false;

            StartCoroutine(AbilityOnCooldown(ability, abilityNumber));
        }

        private IEnumerator AbilityOnCooldown(Ability ability, int abilityNumber)
        {
            yield return new WaitForSeconds(ability.Cooldown);

            _abilityAvailability[abilityNumber] = true;
        }
    }
}