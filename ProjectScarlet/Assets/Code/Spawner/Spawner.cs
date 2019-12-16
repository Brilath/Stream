using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ProjectScarlet
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> npcs;

        [SerializeField] private float spawnCooldown =  15f;
        [SerializeField] private float spawnCountdown;
        [SerializeField] private AttackPath _attackPath;

        private void Awake() 
        {
            spawnCountdown = spawnCooldown;
            _attackPath = GetComponentInChildren<AttackPath>();
        }

        // Start is called before the first frame update
        void Start()
        {

        }    

        // Update is called once per frame
        void Update()
        {
            spawnCountdown -= Time.deltaTime;

            if(spawnCountdown <= 0)
            {
                StartCoroutine(Spawn());
                spawnCountdown = spawnCooldown;
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