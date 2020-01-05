using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> npcs;

        [SerializeField] private float _spawnCooldown =  15f;
        [SerializeField] private float _spawnCountdown;
        [SerializeField] private AttackPath _attackPath;
        [SerializeField] private bool _spawnOnce = false;
        [SerializeField] private bool _spawnUnitsOnceDead = false;
        [SerializeField] private bool _canSpawn = true;
        [SerializeField] private List<GameObject> _spawnedUnits = new List<GameObject>();

        private void Awake() 
        {
            _attackPath = GetComponentInChildren<AttackPath>();
        } 

        // Update is called once per frame
        void Update()
        {
            if (!_canSpawn) return;

            if (_spawnUnitsOnceDead && _spawnedUnits.Count == 0)
            {
                _spawnCountdown -= Time.deltaTime;
                _canSpawn = true;
            }
            else if (!_spawnUnitsOnceDead)
            {
                _spawnCountdown -= Time.deltaTime;
            }

            RefreshUnitList();

            if (_spawnCountdown <= 0 && !_spawnUnitsOnceDead)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;

                if (_spawnOnce)
                    _canSpawn = false;
            }
            else if (_spawnedUnits.Count == 0 &&
                        _spawnUnitsOnceDead &&
                        _spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;
            }

        }

        public void RefreshUnitList()
        {
            for(int i = _spawnedUnits.Count - 1; i >= 0; i--)
            {
                Debug.Log($"{i}");
                if (_spawnedUnits[i] == null)
                    _spawnedUnits.RemoveAt(i);                    
            }
        }

        private IEnumerator Spawn()
        {
            foreach (GameObject npc in npcs)
            {            
                var newNPC = Instantiate(npc, transform.position, transform.rotation);
                newNPC.GetComponent<AIInputController>().SetAttackPath(_attackPath);
                _spawnedUnits.Add(newNPC);
                yield return StartCoroutine(Wait(1f));
            }
        }
        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}