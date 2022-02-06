using UnityEngine;

namespace Instruments
{
    public class FPSController : MonoBehaviour
    {
        private void Awake() => UpdateFPS();

        public void UpdateFPS() 
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = PlayerPrefs.GetInt("FPS");
        }
    }
}
