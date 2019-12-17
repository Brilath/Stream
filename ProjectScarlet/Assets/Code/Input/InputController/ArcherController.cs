using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{

    [SerializeField] private List<Transform> _targets = new List<Transform>();
    [SerializeField] private Vector3 newTarget;
    [SerializeField] private Transform currentTarget;
    [SerializeField] private ArcherMotor _motor;
    [SerializeField] private float attackRange = 10f;

    // Start is called before the first frame update
    void Start()
    {
        _motor = GetComponent<ArcherMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTarget != null && _targets.Count > 0)
        {
            newTarget = currentTarget.position;
        }
        if (currentTarget == null && _targets.Count > 0)
        {
            currentTarget = _targets[0];
            newTarget = currentTarget.position;
        }
        if(_targets.Count == 0)
        {
            currentTarget = null;
            newTarget = Vector3.zero;
        }

        _motor.SetTarget(newTarget);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC" || other.gameObject.tag == "Player")
        {
            _targets.Add(other.transform);
            currentTarget = other.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _targets.Remove(other.transform);
    }
}
