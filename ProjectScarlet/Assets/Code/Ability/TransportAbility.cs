using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Transport Ability", fileName = "Transport Ability")]
    public class TransportAbility : Ability
    {
        [SerializeField] private Vector3 movementDirection;
        [SerializeField] private float distance;
        [SerializeField] private LayerMask collisionLayers;

        public override void ProcessAbility(GameObject unit)
        {
            // Raycast from player forward out to distance
            // if raycast hits anything only transport unit to that distance

            var transform = unit.GetComponent<Transform>();
            var collider = unit.GetComponent<Collider>();
            if(collider == null) return;

            Vector3 rayStartPosition = new Vector3 (transform.position.x, 
                                                    transform.position.y + collider.bounds.extents.y,
                                                    transform.position.z);
            
            RaycastHit hit;
            if(Physics.Raycast(rayStartPosition, transform.TransformDirection(Vector3.forward), out hit, distance, collisionLayers))
            {
                unit.transform.position += Vector3.Scale(transform.TransformDirection(Vector3.forward), movementDirection) * (hit.distance - 1);   
            }
            else
            {
                unit.transform.position += Vector3.Scale(transform.TransformDirection(Vector3.forward), movementDirection)  * distance;
            }
            Debug.DrawRay(rayStartPosition, transform.TransformDirection(Vector3.forward) * distance, Color.red, 30f);
        }
    }
}