using UnityEngine.UI;
using System.Collections;
using UnityEngine;
using System;

namespace ProjectScarlet
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _foregroundImage;
        [SerializeField] private float _updateSpeedSeconds = 0.5f;
        [SerializeField] private float _positionOffset = 2;
        [SerializeField] private Camera _camera;
        [SerializeField] private Health _health;
        [SerializeField] private Transform _transform;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
        }

        public void SetHealth(Health health)
        {
            _health = health;
            _health.OnHealthPctChange += HandleHealthChange;
        }

        private void HandleHealthChange(float pct)
        {           
            StartCoroutine(ChangeToPct(pct));
        }

        private IEnumerator ChangeToPct(float pct)
        {
            float preChangePct = _foregroundImage.fillAmount;
            float elapsed = 0f;

            while(elapsed < _updateSpeedSeconds)
            {
                elapsed += Time.deltaTime;
                _foregroundImage.fillAmount = Mathf.Lerp(preChangePct, pct, elapsed / _updateSpeedSeconds);
                yield return null;
            }

            _foregroundImage.fillAmount = pct;
        }

        private void LateUpdate()
        {
            _transform.position = _camera.WorldToScreenPoint(_health.transform.position +
                Vector3.up * _positionOffset);
        }
    }
}