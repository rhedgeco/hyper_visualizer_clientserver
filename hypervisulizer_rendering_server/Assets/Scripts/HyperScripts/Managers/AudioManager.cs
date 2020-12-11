using UnityEngine;

namespace HyperScripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        public static bool Playing { get; private set; } = false;

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

        public static bool TogglePlayPause()
        {
            Playing = !Playing;
            return Playing;
        }
    }
}
