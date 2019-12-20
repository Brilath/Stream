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

        private void Awake()
        {
            _player = GameObject.FindGameObjectWithTag("Player");
            _health = _player.GetComponent<Health>();

            _health.OnDamage += HandleDamage;
            _health.OnHeal += HandleHeal;
        }

        private void HandleHeal()
        {
            _healthOrb.fillAmount = _health.GetPercentage();
        }

        private void HandleDamage()
        {
            _healthOrb.fillAmount = _health.GetPercentage();
        }
    }
}