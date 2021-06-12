using UnityEngine;

public class FPSController : MonoBehaviour
{
    private void Awake() => UpdateFPS();

    public void UpdateFPS() => Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
}
