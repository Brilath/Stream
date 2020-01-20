using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IMotor : MonoBehaviour
{
    [SerializeField] private float _modifier;
    public float Modifier{ get{ return _modifier; } set { _modifier = value; } }
}
