using UnityEngine;
using UnityEngine.Events;

namespace HyperEngine
{
    public class HyperCore
    {
        private static Camera _mainCamera;
        internal static HyperEvent UpdateFrame { get; } = new HyperEvent();
        internal static CameraChangedEvent CameraChanged { get; } = new CameraChangedEvent();
        
        public static float Time { get; internal set; }
        public static float TotalTime { get; internal set; }

        public static Camera MainCamera
        {
            get => _mainCamera;
            internal set { _mainCamera = value; }
        }

        public static void ConnectFrameUpdate(UnityAction<HyperValues> listener)
        {
            UpdateFrame.AddListener(listener);
        }

        public static void ConnectMainCameraChanged(UnityAction<Camera> listener)
        {
            CameraChanged.AddListener(listener);
        }
    }

    public class CameraChangedEvent : UnityEvent<Camera>
    {
    }
}