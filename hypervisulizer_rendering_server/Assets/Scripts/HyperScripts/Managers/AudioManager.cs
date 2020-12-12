using System.IO;
using HyperScripts.Threading;
using HyperScripts.Threading.Workers;
using UnityEngine;

namespace HyperScripts.Managers
{
    public class AudioManager : MonoBehaviour
    {
        private static AudioManager _instance;

        private static AudioSource _source;
        private static float[] _samples;

        private void Awake()
        {
            if (_instance == null) _instance = this;
            if (_instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(_instance);
            _source = GetComponent<AudioSource>();
        }

        private void Update()
        {
            ThreadSafe.MainThreadCallback();
        }

        internal static void ImportAudioThreaded(string path)
        {
            AudioDecodeWorker worker = new AudioDecodeWorker(path,
                Path.Combine(Application.persistentDataPath, "TempConversion"),
                (sampleArray, channels, sampleRate) =>
                {
                    AudioClip clip = AudioClip.Create(Path.GetFileNameWithoutExtension(path),
                        sampleArray.Length / channels, channels, sampleRate, false);
                    clip.SetData(sampleArray, 0);
                    _source.clip = clip;
                    _samples = sampleArray;
                });
            HyperThreadDispatcher.StartWorker(worker);
        }

        public static class ThreadSafe
        {
            private static bool Playing { get; set; } = false;
            private static bool StopPlaying { get; set; } = false;
            private static string Import { get; set; } = "";
            
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
                StopPlaying = true;
            }

            public static void MainThreadCallback()
            {
                if (Playing && !_source.isPlaying)
                {
                    _source.Play();
                    ApiHandler.SendWebsocketMessage("play");
                }

                if (!Playing && _source.isPlaying)
                {
                    _source.Pause();
                    ApiHandler.SendWebsocketMessage("pause");
                }
                
                if (StopPlaying)
                {
                    Playing = false;
                    StopPlaying = false;
                    _source.Stop();
                    _source.timeSamples = 0;
                    ApiHandler.SendWebsocketMessage("stop");
                }
                
                if (Import != "")
                {
                    ImportAudioThreaded(Import);
                    Import = "";
                }
            }
        }
    }
}