using UnityEngine;

namespace ProjectScarlet
{
    public class CameraZoom : MonoBehaviour
    {

        public float zoom_Sensitivity = 15f;
        public float zoom_Speed = 20f;
        public float zoom_Min = 30f;
        public float zoom_Max = 70f;

        private float z;

        private Camera mainCamera;

        // Start is called before the first frame update
        void Start()
        {
            mainCamera = GetComponent<Camera>();
            z = mainCamera.fieldOfView;

        }

        // Update is called once per frame
        void Update()
        {
            z -= Input.GetAxis("Mouse ScrollWheel") * zoom_Sensitivity;
            z = Mathf.Clamp(z, zoom_Min, zoom_Max);
        }

        // Update will be completed when this is called
        private void LateUpdate() {
            mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, z, Time.deltaTime * zoom_Speed);    
        }
    }
}