using UnityEngine;

public class FPSController : MonoBehaviour
{
    public int fps;

    private void Start()
    {
        Application.targetFrameRate = fps;
    }
}
