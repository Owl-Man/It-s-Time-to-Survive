using UnityEngine;

public class SoundController : MonoBehaviour
{
    public GameObject Music;
    
    private void Start() 
    {
        if (PlayerPrefs.GetInt("Music") == 0) UpdateMusicState(false);
        if (PlayerPrefs.GetInt("Music") == 1) UpdateMusicState(true);
    }

    public void UpdateMusicState(bool state) => Music.SetActive(state);
}
