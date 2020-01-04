using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class CharacterBaseStats : MonoBehaviour
    {
        [Range(1, 16)]
        [SerializeField] private int _startingLevel = 1;
        [SerializeField] private int _currentLevel;
        [SerializeField] private CharacterClass _characterClass;
        [SerializeField] private CharacterProgression _progression;
        [SerializeField] private Health _health;
        [SerializeField] private LayerMask _grantExperienceLayer;

        public int StartingLevel { get { return _startingLevel; } set { _startingLevel = value; } }

        private void Awake()
        {
            _currentLevel = _startingLevel;
            _health = GetComponent<Health>();

            _health.OnDeath += HandleExperienceReward;
        }

        public float GetHealth()
        {
            return _progression.GetHealth(_characterClass, _currentLevel);
        }

        public float GetExperience()
        {
            return _progression.GetExperience(_characterClass, _currentLevel);
        }

        public void IncreaseLevel()
        {
            _currentLevel++;
        }

        private void HandleExperienceReward()
        {
            Collider[] targets = Physics.OverlapSphere(_health.transform.position, 20f, _grantExperienceLayer);

            foreach(Collider target in targets)
            {
                Experience experience = target.GetComponent<Experience>();

                if(experience != null)
                {
                    experience.AddExperience(GetExperienceReward());
                }
            }
        }

        public float GetExperienceReward()
        {
            return 10;
        }
    }
}