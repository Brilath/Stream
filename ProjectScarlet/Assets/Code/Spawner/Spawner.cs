using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected bool _playerSpawner = false;
        [SerializeField] protected List<GameObject> units;
        [SerializeField] protected List<GameObject> _spawnedUnits = new List<GameObject>();
        [SerializeField] protected float _spawnCooldown = 15f;        
        [SerializeField] protected AttackPath _attackPath;                

        private void Awake() 
        {
            if(!_playerSpawner)
                _attackPath = GetComponentInChildren<AttackPath>();
        }

        void Update()
        {
            
        }

        protected IEnumerator Spawn()
        {
            foreach (GameObject unit in units)
            {            
                var newUnit = Instantiate(unit, transform.position, transform.rotation);                
                _spawnedUnits.Add(newUnit);

                if(!_playerSpawner)
                    newUnit.GetComponent<AIInputController>().SetAttackPath(_attackPath);

                yield return StartCoroutine(Wait(1f));
            }
        }

        protected IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}