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
        [SerializeField] private IEnumerator _coroutine;

        private void Awake()
        {
            _camera = Camera.main;
            _transform = GetComponent<Transform>();
            _coroutine = ChangeToPct(1);
        }
        private void OnEnable() 
        {
            if(_health != null)
                _health.OnHealthPctChange += HandleHealthChange;   
        }

        public void SetHealth(Health health)
        {
            _health = health;
            _health.OnHealthPctChange += HandleHealthChange;
        }

        private void HandleHealthChange(float pct)
        {            
            _coroutine = ChangeToPct(pct);

            if (gameObject.activeSelf)
                StartCoroutine(_coroutine);
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

        private void OnDisable()
        {
            _health.OnHealthPctChange -= HandleHealthChange;
            if(_coroutine != null)
                StopCoroutine(_coroutine);
        }
    }
}