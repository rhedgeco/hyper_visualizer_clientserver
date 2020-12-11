using UnityEngine;

namespace HyperScripts.Managers
{
    public class DisplayManager : MonoBehaviour
    {
        private static DisplayManager _instance;
        
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
