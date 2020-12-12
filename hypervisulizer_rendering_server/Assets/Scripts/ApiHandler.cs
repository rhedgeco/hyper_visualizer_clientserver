using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using Fleck;
using HyperScripts.Threading;
using LinkApi;
using Microsoft.Owin.Hosting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    private static ApiHandler _instance;

    private static IDisposable _webApp;
    private static WebsocketHandler _wsApp;
    private static bool _mainSceneReload;

    [SerializeField] private Text initialText;

    private void Awake()
    {
        if (!_instance) _instance = this;
        if (_instance != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ArgParser parser = new ArgParser();
        string apiPort = parser.GetArg("--api-port");
        string wsPort = parser.GetArg("--ws-port");
        if (apiPort == null) apiPort = "9000";
        if (wsPort == null) wsPort = "9001";

        CacheWindowHandle();
        string apiBaseAddress = $"http://localhost:{apiPort}/";
        string wsBaseAddress = $"http://0.0.0.0:{wsPort}/";

        _webApp = WebApp.Start<Startup>(apiBaseAddress);
        _wsApp = new WebsocketHandler(wsBaseAddress);
    }

    private void Update()
    {
        if (_mainSceneReload)
        {
            _mainSceneReload = false;
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OnApplicationQuit()
    {
        _webApp?.Dispose();
        _wsApp?.Dispose();
    }

    public static void SendWebsocketMessage(string message)
    {
        _wsApp.SendMessage(message);
    }

    public static class ThreadSafe
    {
        public static void ReloadMainScene()
        {
            _mainSceneReload = true;
        }
    }

    #region HwndHandler

    public static int WindowHandle { get; private set; } = 0;

    private static string _unityWindowClassName = "UnityWndClass";

    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpEnumFunc, IntPtr lParam);

    private static int CacheWindowHandle()
    {
        uint threadId = GetCurrentThreadId();
        EnumThreadWindows(threadId, (hWnd, lparam) =>
        {
            var classText = new StringBuilder(_unityWindowClassName.Length + 1);
            GetClassName(hWnd, classText, classText.Capacity);
            if (classText.ToString().Equals(_unityWindowClassName))
            {
                WindowHandle = (int) hWnd;
                return false;
            }

            return true;
        }, IntPtr.Zero);

        return WindowHandle;
    }

    #endregion
}