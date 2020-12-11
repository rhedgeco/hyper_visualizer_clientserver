using System.Web.Http;
using HyperScripts.Managers;
using UnityEngine.SceneManagement;

namespace LinkApi
{
    public class RouteController : ApiController
    {
        [Route("api/hwnd")]
        [HttpGet]
        public int GetHwnd()
        {
            return ApiHandler.WindowHandle;
        }

        [Route("api/connection")]
        [HttpPost]
        public bool ConnectClient()
        {
            SceneManager.LoadScene("MainScene");
            return true;
        }

        [Route("audio/play_pause")]
        [HttpPost]
        public bool TogglePlayPause()
        {
            ApiThreadCollector.QueueFunction(AudioManager.TogglePlayPause);
            return false;
        }
    }
}