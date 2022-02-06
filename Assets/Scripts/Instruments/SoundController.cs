using UnityEngine;

namespace Instruments
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private GameObject Music;
    
        private void Start() 
        {
            if (PlayerPrefs.GetInt("Music") == 0) UpdateMusicState(false);
            else if (PlayerPrefs.GetInt("Music") == 1) UpdateMusicState(true);
        }

        public void UpdateMusicState(bool state) => Music.SetActive(state);
    }
}
