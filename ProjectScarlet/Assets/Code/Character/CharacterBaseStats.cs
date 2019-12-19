using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class CharacterBaseStats : MonoBehaviour
    {
        [Range(1, 16)]
        [SerializeField] int _startingLevel = 1;
        [SerializeField] int _currentLevel;
        [SerializeField] CharacterClass _characterClass;
        [SerializeField] CharacterProgression _progression;

        private void Awake()
        {
            _currentLevel = _startingLevel;
        }

        public float GetHealth()
        {
            return _progression.GetHealth(_characterClass, _currentLevel);
        }

        public float GetExperienceReward()
        {
            return 10;
        }
    }
}