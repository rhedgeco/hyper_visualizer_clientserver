using System.Web.Http;
using UnityEngine.SceneManagement;

namespace LinkApi
{
    public class ConnectionController : ApiController
    {
        // GET api/connection
        public void Post()
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}