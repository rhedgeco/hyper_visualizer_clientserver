using UnityEngine;

namespace HyperEngine
{
    [RequireComponent(typeof(Camera))]
    public class MainHyperCamera : MonoBehaviour
    {
        private void Awake()
        {
            HyperCore.MainCamera = GetComponent<Camera>();
        }
    }
}