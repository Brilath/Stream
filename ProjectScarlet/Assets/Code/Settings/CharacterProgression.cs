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
                Debug.Log($"Level: {level}....health {_progressionClasses.health[level - 1]}");
                baseHealth = _progressionClasses.health[level - 1];
            }

            return baseHealth;
        }
    }
}