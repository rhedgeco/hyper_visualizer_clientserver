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



    #endregion
}