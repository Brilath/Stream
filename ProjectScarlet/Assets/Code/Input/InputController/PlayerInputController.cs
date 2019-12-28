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

            if(Input.GetKeyDown(KeyCode.Mouse0))
            {
                Animator anim =GetComponentInChildren<Animator>();
                if(anim != null)
                {
                    
                    Weapon currentWeapon = GetComponent<Fighter>().CurrentWeapon;
                    if (currentWeapon.CanAttack())
                    {
                        anim.SetTrigger("attack");                        
                        StartCoroutine(AttackDelay(currentWeapon.DelayTime, currentWeapon));                        
                    }
                }
            }

            settings.MoveDirection = SetMoveDirection();
        }

        private IEnumerator AttackDelay(float seconds, Weapon weapon)
        {
            Debug.Log("Before Attack delay");
            motor.CanMove = false;
            motor.Stop();

            GameObject spawnedWeapon = GetComponent<Fighter>().SpawnedWeapon;

            Animator weaponAnimator = spawnedWeapon.GetComponent<Animator>();
            if (weaponAnimator != null)
            {
                weaponAnimator.SetTrigger("attack");
            }

            Transform attackPoint = spawnedWeapon.GetComponentInChildren<Transform>();
            weapon.NextAttack = Time.time + weapon.AttackSpeed;

            yield return new WaitForSeconds(seconds);

            Debug.Log("Attack delay over");
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