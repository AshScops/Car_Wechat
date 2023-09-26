using UnityEngine;

namespace QFramework.Car
{
    public class UILookAtCamera : MonoBehaviour
    {
        void Update()
        {
            transform.rotation = Camera.main.transform.rotation;
        }
    }
}