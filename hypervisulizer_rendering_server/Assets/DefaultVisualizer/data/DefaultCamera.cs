using System;
using HyperScripts;
using HyperScripts.Managers;
using UnityEngine;

namespace DefaultVisualizer.data
{
    [RequireComponent(typeof(Camera))]
    public class DefaultCamera : MonoBehaviour
    {
        private void Start()
        {
            RenderingManager.ConnectCamera(GetComponent<Camera>());
        }
    }
}
