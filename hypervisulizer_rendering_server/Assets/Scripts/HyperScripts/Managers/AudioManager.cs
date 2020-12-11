using UnityEngine;

namespace HyperScripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        private static bool Playing { get; set; } = false;
        private static string Import { get; set; } = "";

        private void Awake()
        {
            if (_instance == null) _instance = this;
            if (_instance != this)
            {
                Destroy(_instance);
                return;
            }

            DontDestroyOnLoad(_instance);
        }

        private void Update()
        {
            if(Playing) RenderingManager.RenderFrame();
        }

        public static class ThreadSafe
        {
            public static string ImportAudio(string filename)
            {
                Import = filename;
                return filename;
            }
            
            public static bool TogglePlayPause()
            {
                Playing = !Playing;
                return Playing;
            }

            public static void Stop()
            {
                Playing = false;
            }
        }
    }
}