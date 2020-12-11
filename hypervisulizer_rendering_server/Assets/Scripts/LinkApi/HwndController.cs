using System.Web.Http;
using UnityEngine;

namespace LinkApi
{
    public class HwndController : ApiController
    {
        // GET api/hwnd
        public int Get()
        {
            return ApiHandler.WindowHandle;
        }
    }
}