using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class OneTimeSpawner : Spawner
    {
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(StartSpawn());
        }

        private IEnumerator StartSpawn()
        {
            yield return new WaitForSeconds(_spawnCooldown);

            StartCoroutine(Spawn());
        }
    }
}