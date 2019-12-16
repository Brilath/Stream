using UnityEngine;

namespace ProjectScarlet
{
    public class MouseLock : MonoBehaviour
    {

        private static bool mouseLocked;

        public static bool MouseLocked{
            get {return mouseLocked;}
            set {
                mouseLocked = value;

                if(mouseLocked){
                    Cursor.lockState = CursorLockMode.Locked;
                }
                else{
                    Cursor.lockState = CursorLockMode.None;
                }
            }
        }
    }
}