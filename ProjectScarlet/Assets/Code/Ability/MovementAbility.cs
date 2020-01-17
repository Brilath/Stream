using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace ProjectScarlet
{
    [CreateAssetMenu(menuName = "Ability/Movement Ability", fileName = "Movement Ability")]
    public class MovementAbility : Ability
    {
        [SerializeField] private float movementPower;
        [SerializeField] private Vector3 movementDirection;        

        public MovementAbility()
        {
            Type = AbilityType.Movement;
        }

        public override void ProcessAbility(GameObject unit)
        {
            Rigidbody _body = unit.GetComponent<Rigidbody>();
            Vector3 unitDirection = unit.GetComponent<PlayerInputController>().SetMoveDirection();

            float x = unit.transform.forward.x * movementPower * movementDirection.x;
            float y = movementDirection.y;
            float z = unit.transform.forward.z *movementPower * movementDirection.z;

            Animator anim = unit.GetComponentInChildren<Animator>();

            if (anim != null)
                anim.Play(Clip.name);

            if(unitDirection.magnitude > 0.01f)
            {
                x = unitDirection.x * movementDirection.x;                
                z = unitDirection.z * movementDirection.z;
            }

            Vector3 direction = new Vector3(x, y, z);

            _body.AddForce(direction * movementPower
                    , ForceMode.VelocityChange);
        }   
    }
}