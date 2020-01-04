using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Character/Progression", fileName = "ClassProgression")]
    public class CharacterProgression : ScriptableObject
    {
        [SerializeField] private ProgressCharacterClass _progressionClasses = null;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            float baseHealth = 0;

            if (_progressionClasses.characterClass == characterClass &&
                    (level > 0 && level < _progressionClasses.health.Length))
            {
                baseHealth = _progressionClasses.health[level - 1];
            }

            return baseHealth;
        }

        public float GetExperience(CharacterClass characterClass, int level)
        {
            float baseExperience = Mathf.Infinity;

            if (_progressionClasses.characterClass == characterClass &&
                    (level > 0 && level < _progressionClasses.health.Length))
            {
                baseExperience = _progressionClasses.experience[level - 1];
            }

            return baseExperience;
        }
    }
}