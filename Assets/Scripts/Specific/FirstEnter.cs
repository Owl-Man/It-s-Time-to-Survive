using UnityEngine;
using UnityEngine.SceneManagement;

public class FirstEnter : MonoBehaviour 
{
    private void Awake() 
    {
        if (PlayerPrefs.GetInt("isntFirstEnter") != 1) 
        {
            PlayerPrefs.SetInt("isntFirstEnter", 1);
            SceneManager.LoadScene("Intro");
            return;
        }

        if (PlayerPrefs.GetInt("isntAfterIntroEnter") != 1) StabilizitionValues();
    }

    private void StabilizitionValues() 
    {
        PlayerPrefs.SetInt("Kills", 0);
        PlayerPrefs.SetInt("Days", 1);
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetInt("EXP", 0);

        PlayerPrefs.SetInt("isntAfterIntroEnter", 1);
    }
}