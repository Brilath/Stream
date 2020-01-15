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
        [SerializeField] private int castTime = 20;

        public MovementAbility()
        {
            Type = AbilityType.Movement;
        }

        public override async void ProcessAbility(GameObject unit)
        {
            Rigidbody _body = unit.GetComponent<Rigidbody>();
            Vector3 unitDirection = unit.GetComponent<PlayerInputController>().SetMoveDirection();

            float x = unit.transform.forward.x * movementPower * movementDirection.x;
            float y = movementDirection.y;
            float z = unit.transform.forward.z *movementPower * movementDirection.z;

            Animator anim = unit.GetComponentInChildren<Animator>();

            var currentOverrideController = anim.runtimeAnimatorController as AnimatorOverrideController;

            if (overrideController != null)
            {
                anim.runtimeAnimatorController = overrideController;
            }
            else if (currentOverrideController != null)
            {
                anim.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }

            anim.SetTrigger("ability");

            if(unitDirection.magnitude > 0.01f)
            {
                x = unitDirection.x * movementDirection.x;                
                z = unitDirection.z * movementDirection.z;
            }

            Vector3 direction = new Vector3(x, y, z);
    
            Debug.Log("Player Movement: " + unitDirection);
            Debug.Log("Unit direction: " + direction);

            _body.AddForce(direction * movementPower
                    , ForceMode.VelocityChange);

            var results = await WaitSeconds(castTime);

            anim.runtimeAnimatorController = currentOverrideController;
        }

        private async Task<int> WaitSeconds(int seconds)
        {
            await Task.Delay(seconds * 1000);

            return 0;
        }     
    }
}