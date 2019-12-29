﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System;

namespace ProjectScarlet
{
    public class NPCMotor : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 3.25f;
        [SerializeField] private float _turnSpeed = 1000f;

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Transform _transform;
        [SerializeField] private Animator _animator;

        [SerializeField] private string FORWARD_SPEED = "forwardSpeed";


        private void Awake() 
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _transform = GetComponent<Transform>();
            _animator = GetComponentInChildren<Animator>();
        }

        private void Update() 
        {
            // Vector3 velocity = _navMeshAgent.velocity;
            // Vector3 localVelocity = _transform.InverseTransformDirection(velocity);

            _animator.SetFloat(FORWARD_SPEED, _navMeshAgent.speed);
        }

        public void MoveTo(Vector3 destination)
        {
            //Debug.Log($"Dest: {destination}");
            _navMeshAgent.destination = destination;
            _navMeshAgent.speed = _maxSpeed;
            _navMeshAgent.isStopped = false;
        }

        public void RotateTowards(Vector3 target)
        {
            _transform.rotation = Quaternion.Slerp(_transform.rotation, Quaternion.LookRotation(target), _turnSpeed * Time.deltaTime);
        }

        public void Stop()
        {
            _navMeshAgent.isStopped = true;
            _navMeshAgent.speed = 0f;
        }
    }
}