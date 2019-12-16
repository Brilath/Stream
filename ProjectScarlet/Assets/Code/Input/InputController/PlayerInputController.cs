using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectScarlet
{
    public class PlayerInputController : MonoBehaviour, ICharacterInput
    {

        [SerializeField] private PlayerKeybinding keybinding;
        [SerializeField] private CharacterSettings settings;
        [SerializeField] private CharacterMotor motor;
        private Camera playerCamera;

        private void Awake()
        {
            playerCamera = Camera.main;
            motor = GetComponent<CharacterMotor>();
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            Tick();
        }

        public void Tick()
        {
            //Debug.Log("Ticking");

            foreach(Keybind keybind in keybinding.Keybindings)
            {
                if(Input.GetKeyDown(keybind.Key))
                {
                    keybind.Command.Execute(motor);
                    break;
                }
            }

            settings.MoveDirection = SetMoveDirection();
        }

        private Vector3 SetMoveDirection()
        {
            Vector3 direction;
            Vector3 forward = Quaternion.AngleAxis(-90f, Vector3.up) * playerCamera.transform.right;

            direction = forward * Input.GetAxis("Vertical");
            direction +=  playerCamera.transform.right *  Input.GetAxis("Horizontal");
            direction.Normalize();
            
            direction *= settings.MaxSpeed;

            return direction;
        }
    }
}