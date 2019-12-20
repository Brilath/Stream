using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : MonoBehaviour
{

    [SerializeField] private List<Transform> _targets = new List<Transform>();
    [SerializeField] private Transform _transform;
    [SerializeField] private Vector3 _newTarget;
    [SerializeField] private Transform _currentTarget;
    [SerializeField] private ArcherMotor _motor;
    [SerializeField] private float _attackRange = 10f;
    [SerializeField] private LayerMask _attackLayer;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _motor = GetComponent<ArcherMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        SetTarget();

        _motor.SetTarget(_newTarget);
    }

    private void FixedUpdate()
    {
        RefreshTargets();
    }

    private void SetTarget()
    {
        if (_currentTarget != null && _targets.Count > 0)
        {
            _newTarget = _currentTarget.position;
        }
        if (_currentTarget == null && _targets.Count > 0)
        {
            _currentTarget = _targets[0];
            _newTarget = _currentTarget.position;
        }
        if (_targets.Count == 0)
        {
            _currentTarget = null;
            _newTarget = Vector3.zero;
        }
    }

    private void RefreshTargets()
    {
        Collider[] enemies = Physics.OverlapSphere(_transform.position, _attackRange, _attackLayer);

        _targets.Clear();

        foreach(Collider enemy in enemies)
        {
            _targets.Add(enemy.transform);
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "NPC" || other.gameObject.tag == "Player")
    //    {
    //        _targets.Add(other.transform);
    //        _currentTarget = other.transform;
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    _targets.Remove(other.transform);
    //}
}
