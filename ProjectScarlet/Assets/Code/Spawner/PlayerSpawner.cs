using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class PlayerSpawner : Spawner
    {
        [SerializeField] protected float _spawnCountdown;

        [SerializeField] private List<GameObject> _playerUnits = new List<GameObject>();
        [SerializeField] private int _selectedUnit;

        void Update()
        {
            
            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                SpawnDifferentPlayerType();
            }
            RefreshUnitList();

            if (_spawnedUnits.Count == 0)
            {
                _spawnCountdown -= Time.deltaTime;
            }

            if (_spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;
            }
        }

        private void SpawnDifferentPlayerType()
        {
            _selectedUnit++;
            if(_selectedUnit >= _playerUnits.Count)
                _selectedUnit = 0;

            _units[0] = _playerUnits[_selectedUnit];
            Destroy(_spawnedUnits[0]);
            _spawnedUnits.RemoveAt(0);

            StartCoroutine(Spawn());
        }
    }
}