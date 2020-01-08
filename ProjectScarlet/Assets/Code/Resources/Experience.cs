using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float _maxExperience;
        [SerializeField] private float _currentExperience;
        [SerializeField] private CharacterBaseStats _baseStats;

        public float CurrentExperience { get { return _currentExperience; } set { _currentExperience = value; } }

        public event Action OnLevelUp = delegate { };
        public event Action IncreaseExperience = delegate{};
        
        void Start()
        {
            SetupExperience();
        }

        private void OnEnable()
        {
            SetupExperience();
        }

        private void SetupExperience()
        {
            _baseStats = GetComponent<CharacterBaseStats>();
            _maxExperience = GetBaseExperience();            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddExperience(float xp)
        {
            _currentExperience += xp;

            if(_currentExperience >= _maxExperience)
            {
                LevelUp();
            }

            IncreaseExperience();
        }

        private void LevelUp()
        {
            _currentExperience -= _maxExperience;
            _baseStats.IncreaseLevel();
            _maxExperience = GetBaseExperience();
            OnLevelUp();
        }

        private float GetBaseExperience()
        {
            return _baseStats.GetExperience();
        }

        public int GetCurrentLevel()
        {
            return _baseStats.CurrentLevel;
        }

        public float GetPercentage()
        {
            return CurrentExperience / GetBaseExperience();
        }
    }
}