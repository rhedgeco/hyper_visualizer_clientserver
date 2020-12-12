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
            private static bool State { get; set; } = false;
            private static bool Play { get; set; } = false;
            private static bool Pause { get; set; } = false;
            private static bool Stop { get; set; } = false;
            private static string Import { get; set; } = "";
            
            public static void ImportAudio(string filename)
            {
                Import = filename;
            }
            
            public static void TogglePlayPause()
            {
                if (State) Pause = true;
                else Play = true;
            }

            public static void StopAudio()
            {
                Stop = true;
            }

            private static bool _lastAudioState = false;
            public static void MainThreadCallback()
            {
                if (Play && !_source.isPlaying)
                {
                    _source.Play();
                    Play = false;
                    ApiHandler.SendWebsocketMessage("_play");
                    _lastAudioState = _source.isPlaying;
                }

                if (Pause && _source.isPlaying)
                {
                    _source.Pause();
                    Pause = false;
                    ApiHandler.SendWebsocketMessage("_pause");
                    _lastAudioState = _source.isPlaying;
                }
                
                if (Stop)
                {
                    _source.Stop();
                    _source.timeSamples = 0;
                    Stop = false;
                    ApiHandler.SendWebsocketMessage("_stop");
                    _lastAudioState = _source.isPlaying;
                }
                
                if (Import != "")
                {
                    ImportAudioThreaded(Import);
                    ApiHandler.SendWebsocketMessage($"_import_audio?{Import}");
                    Import = "";
                }
                
                if (!_source.isPlaying && _lastAudioState) ApiHandler.SendWebsocketMessage("_stop");
                _lastAudioState = _source.isPlaying;
                State = _lastAudioState;
            }
        }
    }
}