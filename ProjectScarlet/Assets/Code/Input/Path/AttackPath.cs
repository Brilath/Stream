using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPath : MonoBehaviour
{

    [SerializeField] private Transform _transform;
    [SerializeField] private Color _color;

    const float WAYPOINT_GIZMO_RADIUS = 0.3f;

    private void Awake() 
    {
        _transform = GetComponent<Transform>();
    }

    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = _color;

        int length = _transform.childCount;

        for(int i = 0; i < length; i++)
        {
            int j = GetNextWaypoint(i);

            Gizmos.DrawSphere(GetWayPoint(i), WAYPOINT_GIZMO_RADIUS);
            Gizmos.DrawLine(GetWayPoint(i), GetWayPoint(j));
        }    
    }

    public int GetNextWaypoint(int index)
    {
        int newIndex = index;

        if(index + 1 != _transform.childCount)
        {
            newIndex = index + 1;
        }

        return newIndex;
    }

    public Vector3 GetWayPoint(int index)
    {
        return _transform.GetChild(index).position;        
    }

}
