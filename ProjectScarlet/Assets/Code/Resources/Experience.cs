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

        public float CurrentExperience { get { return _currentExperience; } set { _currentExperience = value; } }

        public event Action OnLevelUp = delegate { };
        
        void Start()
        {
            SetExperience();
        }

        private void OnEnable()
        {
            SetExperience();
        }

        private void SetExperience()
        {
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
        }

        private void LevelUp()
        {
            _currentExperience -= _maxExperience;
            GetComponent<CharacterBaseStats>().IncreaseLevel();
            SetExperience();
            OnLevelUp();
        }

        private float GetBaseExperience()
        {
            return GetComponent<CharacterBaseStats>().GetExperience();
        }
    }
}