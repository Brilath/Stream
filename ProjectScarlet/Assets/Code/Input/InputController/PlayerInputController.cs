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
        [SerializeField] private Fighter _fighter;
        private Camera playerCamera;

        private void Awake()
        {
            playerCamera = Camera.main;
            motor = GetComponent<CharacterMotor>();
            _fighter = GetComponent<Fighter>();
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

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Animator anim =GetComponentInChildren<Animator>();
                if(anim != null)
                {
                    
                    Weapon currentWeapon = _fighter.CurrentWeapon;
                    if (_fighter.CanAttack())
                    {
                        var oldRotation = transform.rotation;

                        Debug.Log($"Y Rotation: {transform.rotation.y}");
                        anim.SetTrigger("attack");
                        //transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 
                        //    transform.rotation.y + 90f, transform.rotation.z));
                        transform.rotation *=  Quaternion.Euler(0, currentWeapon.TransformRotationOffset, 0);
                        StartCoroutine(AttackDelay(currentWeapon.DelayTime, currentWeapon));                        
                    }
                }
            }

            settings.MoveDirection = SetMoveDirection();
        }

        private IEnumerator AttackDelay(float seconds, Weapon weapon)
        {
            motor.CanMove = false;
            motor.Stop();

            GameObject spawnedWeapon = _fighter.SpawnedWeapon;

            Animator weaponAnimator = spawnedWeapon.GetComponent<Animator>();
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("attack");
            }

            Transform attackPoint = spawnedWeapon.GetComponentInChildren<Transform>();
            _fighter.NextAttack = Time.time + weapon.AttackSpeed;
            
            yield return new WaitForSeconds(seconds);

            weapon.Attack(attackPoint);

            motor.CanMove = true;
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