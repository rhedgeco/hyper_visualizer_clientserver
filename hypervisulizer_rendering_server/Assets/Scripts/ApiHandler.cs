using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Text;
using LinkApi;
using Microsoft.Owin.Hosting;
using UnityEngine;
using UnityEngine.UI;

public class ApiHandler : MonoBehaviour
{
    public static int WindowHandle;
    private static ApiHandler _instance;

    private static IDisposable _webApp;

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
        if (port == null)
        {
            initialText.text = "ERROR:\nCould not get --api-port for starting rendering server\nClosing in 5sec";
            StartCoroutine(CloseIn5());
            return;
        }

        string baseAddress = $"http://localhost:{port}/";
        _webApp = WebApp.Start<Startup>(baseAddress);
    }

    private IEnumerator CloseIn5()
    {
        yield return new WaitForSeconds(5);
        Application.Quit();
    }

    #region HWNDExtrenalAndHandlers

    private static string _unityWindowClassName = "UnityWndClass";

    [DllImport("kernel32.dll")]
    static extern uint GetCurrentThreadId();

    [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
    static extern int GetClassName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

    public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool EnumThreadWindows(uint dwThreadId, EnumWindowsProc lpEnumFunc, IntPtr lParam);

    private static int GetWindowHandle()
    {
        if (WindowHandle != 0) return WindowHandle;

#if UNITY_EDITOR
        _unityWindowClassName = "UnityContaine";
#endif

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

        if (WindowHandle == 0) throw new Exception("Window handle could not be found.");
        return WindowHandle;
    }

    #endregion
}