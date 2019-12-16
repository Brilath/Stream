using UnityEngine;

namespace ProjectScarlet
{
    public class Orbit : MonoBehaviour
    {

        public SphericalVector sphericalVector = new SphericalVector(0, 0, 1);

        protected virtual void Update(){
            transform.position = sphericalVector.Position;
        }
    }
}