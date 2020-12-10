using System;
using System.Runtime.InteropServices;
using System.Text;

public class WindowHandler
{
    public static int WindowHandle
    {
        get
        {
            if (_windowHandle == 0) return GetWindowHandle();
            return _windowHandle;
        }
        private set => _windowHandle = value;
    }

    private static int _windowHandle = 0;

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
}