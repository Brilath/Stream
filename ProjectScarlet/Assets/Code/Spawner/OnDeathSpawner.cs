using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class OnDeathSpawner : Spawner
    {
        [SerializeField] protected float _spawnCountdown;
        
        void Update()
        {
            RefreshUnitList();

            if(_spawnedUnits.Count == 0)
            {
                _spawnCountdown -= Time.deltaTime;
            }

            if(_spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;
            }
        }  
    }
}