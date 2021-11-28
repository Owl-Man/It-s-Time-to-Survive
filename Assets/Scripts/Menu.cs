using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour 
{
    [SerializeField] private string MainPlayScene;

    [Header ("References")]

    [SerializeField] private FPSController fpsCntrl;
    [SerializeField] private SoundController Music;
    [SerializeField] private EffectsController effectsCntrl; 

    [Header ("GameObjects")]

    [SerializeField] private GameObject SettingsPanel, SettingsButton;
    [SerializeField] private GameObject[] galochka;
    [SerializeField] private GameObject MusicOnButton, MusicOffButton;
    [SerializeField] private GameObject EffectsOnObject, EffectsOffObject;

    private void Start() 
    {
        UpdateFPSState(PlayerPrefs.GetInt("IdFPS"));
        UpdateEffectsState();

        Time.timeScale = 1f;
    }

    public void OnPlayButtonClick() => SceneManager.LoadScene(MainPlayScene);

    public void On30FPSButtonClick() => UpdateFPS(30, 0);

    public void On60FPSButtonClick() => UpdateFPS(60, 1);

    public void On90FPSButtonClick() => UpdateFPS(90, 2);

    public void On120FPSButtonClick() => UpdateFPS(120, 3);

    public void OnMusicOnButtonClick() => MusicOn();

    public void OnMusicOffButtonClick() => MusicOff();

    public void OnEffectsOnButtonClick() => EffectsOn();

    public void OnEffectsOffButtonClick() => EffectsOff();

    public void OnExitButtonClick() => Application.Quit();

    public void UpdateEffectsState() 
    {
        effectsCntrl.UpdateEffects();

        if (PlayerPrefs.GetInt("isEffectsEnabled") == 1)
        {
            EffectsOnObject.SetActive(true);
            EffectsOffObject.SetActive(false);
        }
        else 
        {
            EffectsOnObject.SetActive(false);
            EffectsOffObject.SetActive(true);
        }
    }

    private void EffectsOn() 
    {
        PlayerPrefs.SetInt("isEffectsEnabled", 1);

        UpdateEffectsState();
    }

    private void EffectsOff() 
    {
        PlayerPrefs.SetInt("isEffectsEnabled", 0);

        UpdateEffectsState();
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
}