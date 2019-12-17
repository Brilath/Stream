using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMotor : MonoBehaviour
{

    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private Vector3 _target;
    [SerializeField] private Vector3 _orginalRotation;
    [SerializeField] private Vector3 _lookAt;
    [SerializeField] private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _orginalRotation = _transform.position;
    }

    void Update()
    {
        if (_target != Vector3.zero)
        {
            _lookAt = _target - transform.position;
            Rotate(_lookAt);
        }
        else
        {
            _lookAt = _orginalRotation - transform.position;
            Rotate(_lookAt);
        }
    }

    private void Rotate(Vector3 lookAt)
    {
        lookAt.y = 0;
        _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(lookAt), _turnSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }
}
