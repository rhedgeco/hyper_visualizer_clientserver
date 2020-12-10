using System.Web.Http;
using UnityEngine.SceneManagement;

namespace LinkApi
{
    public class HwndController : ApiController
    {
        // GET api/hwnd
        public int Get()
        {
            return WindowHandler.WindowHandle;
        }
    }
}