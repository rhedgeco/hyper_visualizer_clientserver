using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using LinkApi;
using Microsoft.Owin.Hosting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    private static ApiHandler _instance;

    public static IDisposable _webApp;
    private static bool _mainSceneReload;

    [SerializeField] private Text initialText;

    private void Awake()
    {
        if (!_instance) _instance = this;
        if (_instance != this)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        ArgParser parser = new ArgParser();
        string port = parser.GetArg("--api-port");
        if (port == null) port = "9000";

        CacheWindowHandle();
        string baseAddress = $"http://localhost:{port}/";
        _webApp = WebApp.Start<Startup>(baseAddress);
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