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
        public void TogglePlayPause()
        {
            AudioManager.ThreadSafe.TogglePlayPause();
        }
        
        [Route("audio/stop")]
        [HttpPost]
        public void Stop()
        {
            AudioManager.ThreadSafe.StopAudio();
        }

        [Route("audio/import")]
        [HttpPost]
        public void ImportAudio([FromUri] string filename)
        {
            AudioManager.ThreadSafe.ImportAudio(filename);
        }
    }
}