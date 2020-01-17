using UnityEngine;

namespace ProjectScarlet
{
    public class CameraOrbit : Orbit
    {

    public Vector3 target_Offset = new Vector3(0, 2, 0);
    public Vector3 camera_Position_Zoom = new Vector3(-0.5f, 0, 0);
    public float camera_Length = -10f;
    public float camera_Length_Zoom = -5f;
    public Vector2 orbit_Speed = new Vector2(0.01f, 0.01f);
    public Vector2 orbit_Offset = new Vector2(0, -0.8f);
    public Vector2 angle_Offset = new Vector2(0, -0.25f);
    public GameObject target;

    private float zoomValue;
    private Vector3 camera_Position_Temp;
    private Vector3 camera_Position;

    private Transform playerTarget;
    private Camera mainCamera;

    void Start() 
    {

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
        if (playerTarget != null)
        {
            setTarget(playerTarget);
        }            

        sphericalVector.Length = camera_Length;
        sphericalVector.Azimuth = angle_Offset.x;
        sphericalVector.Zenith = angle_Offset.y;

        mainCamera = GetComponent<Camera>();

        camera_Position_Temp = mainCamera.transform.localPosition;
        camera_Position = camera_Position_Temp;

        MouseLock.MouseLocked = true;
        }

        new void Update() 
        {
            if (playerTarget == null)
            {
                playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
                setTarget(playerTarget);
            }

            HandleCamera();
            HandleMouseLocking();           
        }

        public void setTarget(Transform target)
        {
            playerTarget = target;
        }

        void HandleCamera() {
            if(MouseLock.MouseLocked){
                sphericalVector.Azimuth += Input.GetAxis("Mouse X") * orbit_Speed.x;
                sphericalVector.Zenith += Input.GetAxis("Mouse Y") * orbit_Speed.y;
            }
            

            // Will not allow the value to go above 0 and below off y
            sphericalVector.Zenith = Mathf.Clamp(sphericalVector.Zenith + orbit_Offset.x, orbit_Offset.y, 0f);

            float distance_ToObject = zoomValue;
            // Min -distance_ToObject max distance_ToObject
            float delta_Distance = Mathf.Clamp(zoomValue, distance_ToObject, -distance_ToObject);

            sphericalVector.Length += (delta_Distance - sphericalVector.Length);

            Vector3 lookAt = target_Offset;
            lookAt += playerTarget.position;    
            base.Update();

            transform.position += lookAt;
            transform.LookAt(lookAt); // Look at the player

            if (zoomValue == camera_Length_Zoom) {
                Quaternion targetRotation = transform.rotation;

                    targetRotation.x = 0f;
                    targetRotation.y = 0f;
                    playerTarget.rotation = targetRotation;
            }

            camera_Position = camera_Position_Temp;
            zoomValue = camera_Length;
        }

        void HandleMouseLocking()
        {
            if(Input.GetKeyDown(KeyCode.Tab)){
                if (MouseLock.MouseLocked)
                {
                    MouseLock.MouseLocked = false;
                }
                else
                {
                    MouseLock.MouseLocked = true;
                }
            }
        }
    }
}
