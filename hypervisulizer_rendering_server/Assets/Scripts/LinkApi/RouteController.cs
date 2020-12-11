using System.Web.Http;
using HyperScripts.Managers;

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
        public void ConnectClient()
        {
            ApiHandler.ThreadSafe.ReloadMainScene();
        }

        [Route("audio/play_pause")]
        [HttpPost]
        public bool TogglePlayPause()
        {
            return AudioManager.ThreadSafe.TogglePlayPause();
        }
        
        [Route("audio/stop")]
        [HttpPost]
        public void Stop()
        {
            AudioManager.ThreadSafe.Stop();
        }

        [Route("audio/import")]
        [HttpPost]
        public string ImportAudio([FromUri] string filename)
        {
            return AudioManager.ThreadSafe.ImportAudio(filename);
        }
    }
}