using UnityEngine;

[RequireComponent(typeof(Camera))]
public class RenderOnStart : MonoBehaviour
{
    void Start()
    {
        GetComponent<Camera>().Render();
    }
}