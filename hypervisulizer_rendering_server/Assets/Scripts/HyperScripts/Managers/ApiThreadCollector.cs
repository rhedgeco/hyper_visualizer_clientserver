using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace HyperScripts.Managers
{
    public class ApiThreadCollector : MonoBehaviour
    {
        private static ApiThreadCollector _instance;
    
        private static List<UnityAction> _functionStorage = new List<UnityAction>();

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
            if (_functionStorage.Count == 0) return;
            foreach (UnityAction action in _functionStorage)
            {
                action.Invoke();
            }
            _functionStorage.Clear();
        }

        public static void QueueFunction(UnityAction function)
        {
            _functionStorage.Add(function);
        }
    }
}
