using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] protected bool _playerSpawner = false;
        [SerializeField] protected List<GameObject> _units;
        [SerializeField] protected List<GameObject> _spawnedUnits = new List<GameObject>();
        [SerializeField] protected List<GameObject> _availableUnits = new List<GameObject>();
        [SerializeField] protected GameObject _availableUnit;
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
            foreach (GameObject unit in _units)
            {
                // Check if unit is available to spawn
                if(IsUnitAvailable(unit))
                {
                    RecycleUnit();
                }
                // If no unit is available to spawn create a new one
                else
                {
                    CreateNewUnit(unit);
                }

                // short delay between spawns
                yield return StartCoroutine(Wait(1f));
            }
        }

        private bool IsUnitAvailable(GameObject unit)
        {            
            foreach(GameObject aUnit in _availableUnits)
            {
                if(aUnit.GetComponent<CharacterBaseStats>().CharClass == 
                    unit.GetComponent<CharacterBaseStats>().CharClass)
                {
                    _availableUnit = aUnit;
                    return true;
                }
            }
            return false;
        }

        private void RecycleUnit()
        {
            _availableUnits.Remove(_availableUnit);
            _spawnedUnits.Add(_availableUnit);            

            _availableUnit.transform.position = transform.position;
            _availableUnit.transform.rotation = transform.rotation;

            _availableUnit.SetActive(true);
            
            _availableUnit = null;
        }

        private void CreateNewUnit(GameObject unit)
        {
            var newUnit = Instantiate(unit, transform.position, transform.rotation);
            _spawnedUnits.Add(newUnit);

            if (!_playerSpawner)
                newUnit.GetComponent<AIInputController>().SetAttackPath(_attackPath);
        }

        protected void RefreshUnitList()
        {
            for (int i = _spawnedUnits.Count - 1; i >= 0; i--)
            {
                if (!_spawnedUnits[i].activeSelf)
                {
                    _availableUnits.Add(_spawnedUnits[i]);
                    _spawnedUnits.RemoveAt(i);
                }
            }
        }

        protected IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}