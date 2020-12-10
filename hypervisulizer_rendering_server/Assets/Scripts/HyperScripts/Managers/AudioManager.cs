using UnityEngine;

namespace HyperScripts
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

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
    }
}
