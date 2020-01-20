using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherMotor : IMotor
{

    [SerializeField] private float _turnSpeed = 10f;
    [SerializeField] private Vector3 _target;
    [SerializeField] private Quaternion _orginalRotation;
    [SerializeField] private Vector3 _lookAt;
    [SerializeField] private Transform _transform;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
        _orginalRotation = _transform.rotation;
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
            Rotate(_orginalRotation);
        }        
    }

    private void FixedUpdate()
    {
        Vector3 startPosition = new Vector3(_transform.position.x, _transform.position.y + 2f, transform.position.z);

        Debug.DrawRay(startPosition, _lookAt, Color.red);
    }

    private void Rotate(Vector3 lookAt)
    {
        lookAt.y = 0;
        _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(lookAt), _turnSpeed * Time.deltaTime);
    }
    private void Rotate(Quaternion lookAt)
    {
        _transform.rotation = Quaternion.Slerp(_transform.rotation, lookAt, _turnSpeed * Time.deltaTime);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }
}
