using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField] private string MainPlayScene;

    [Header ("References")]

    public FPSController fpsCntrl;

    public SoundController Music;

    [Header ("GameObjects")]

    public GameObject SettingsPanel;

    public GameObject SettingsButton;

    public GameObject[] galochka;

    public GameObject MusicOnButton;
    public GameObject MusicOffButton;

    private void Start() 
    {
        UpdateFPSState(PlayerPrefs.GetInt("IdFPS"));
    }

    public void UpdateFPSState(int id) 
    {
        for (int i = 0; i < galochka.Length; i++) 
        {
            galochka[i].SetActive(false);
        }

        galochka[id].SetActive(true);
    }

    public void UpdateMusicState(int state) 
    {
        if (state == 0) MusicOff();

        if (state == 1) MusicOn();
    }

    public void OnPlayButtonClick() => SceneManager.LoadScene(MainPlayScene);

    public void OnSettingsButtonClick() 
    {
        SettingsPanel.SetActive(true);
        SettingsButton.SetActive(false);
    }

    public void OnBackFromSettingsButtonClick() 
    {
        SettingsPanel.SetActive(false);
        SettingsButton.SetActive(true);
    }

    public void On30FPSButtonClick() => UpdateFPS(30, 0);

    public void On60FPSButtonClick() => UpdateFPS(60, 1);

    public void On90FPSButtonClick() => UpdateFPS(90, 2);

    public void On120FPSButtonClick() => UpdateFPS(120, 3);

    public void OnMusicOnButtonClick() => MusicOn();

    public void OnMusicOffButtonClick() => MusicOff();

    private void MusicOn() 
    {
        PlayerPrefs.SetInt("Music", 0);

        MusicOnButton.SetActive(false);
        MusicOffButton.SetActive(true);

        Music.UpdateMusicState(false);
    }

    private void MusicOff() 
    {
        PlayerPrefs.SetInt("Music", 1);

        MusicOnButton.SetActive(true);
        MusicOffButton.SetActive(false);

        Music.UpdateMusicState(true);
    }

    private void UpdateFPS(int fps, int id) 
    {
        SetFPS(fps);

        PlayerPrefs.SetInt("IdFPS", id);

        UpdateFPSState(id);
    }

    private void SetFPS(int fps) 
    {
        PlayerPrefs.SetInt("FPS", fps);
        fpsCntrl.UpdateFPS();
    }

    public void OnResetButtonClick() 
    {
        PlayerPrefs.SetInt("isntFirstEnter", 0);
        PlayerPrefs.SetInt("isntAfterIntroEnter", 0);

        SceneManager.LoadScene("Menu");
    }
    
    public void OnExitButtonClick() => Application.Quit();
}