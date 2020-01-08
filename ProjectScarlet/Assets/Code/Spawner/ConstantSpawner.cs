using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class ConstantSpawner : Spawner
    {
        [SerializeField] protected float _spawnCountdown;

        void Update()
        {
            if(_spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;
            }
            {
                _spawnCountdown -= Time.deltaTime;
            }
        }
    }
}