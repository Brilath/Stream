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
        [SerializeField] private bool _canSpawn = true;

        private void Awake() 
        {
            //_spawnCountdown = _spawnCooldown;
            _attackPath = GetComponentInChildren<AttackPath>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }    

        // Update is called once per frame
        void Update()
        {
            if (!_canSpawn) return;

            _spawnCountdown -= Time.deltaTime;

            if(_spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                _spawnCountdown = _spawnCooldown;

                if (_spawnOnce)
                    _canSpawn = false;
            }            
        }

        private IEnumerator Spawn()
        {
            foreach (GameObject npc in npcs)
            {            
                var newNPC = Instantiate(npc, transform.position, transform.rotation);
                newNPC.GetComponent<AIInputController>().SetAttackPath(_attackPath);
                yield return StartCoroutine(Wait(1f));
            }
        }
        private IEnumerator Wait(float duration)
        {
            yield return new WaitForSeconds(duration);
        }
    }
}