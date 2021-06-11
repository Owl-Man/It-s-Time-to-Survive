using System;
using UnityEngine;
using UnityEngine.UI;

public class Values : MonoBehaviour 
{
    [SerializeField] private Text KillsText;
    [SerializeField] private Text DaysText;
    [SerializeField] private Text LevelText;

    public int Kills;
    public int Days;
    public int Level;
    public int EXP;
    
    public Image LevelBar;

    public RedMoonWave RedMoon;


    private void Start() => SyncValues();

    private void SyncValues() 
    {
        Kills = PlayerPrefs.GetInt("Kills");
        Days = PlayerPrefs.GetInt("Days");
        Level = PlayerPrefs.GetInt("Level");
        EXP = PlayerPrefs.GetInt("EXP");

        UpdateKillsValues();
        UpdateDaysValues();
        UpdateValuesOfLevel();
    }

    public void ChangesKillsValue(int count)
    {
        Kills += count;
        UpdateKillsValues();
        PlayerPrefs.SetInt("Kills", Kills);
    }

    public void ChangeDaysValue(int count) 
    {
        Days += count;
        UpdateDaysValues();
        PlayerPrefs.SetInt("Days", Days);
        CheckForRedMoonDay();
    }

    public void ChangeEXPValue(int count) 
    {
        EXP += count;
        UpdateLevelValues();
    }

    private void UpdateKillsValues() => KillsText.text = Kills.ToString();

    private void UpdateDaysValues() => DaysText.text = Days.ToString();

    private void UpdateLevelValues() //Обновляет общие значения уровней
    {
        if (EXP >= 100) 
        {
            Level++;
            EXP -= 100;

            PlayerPrefs.SetInt("Level", Level);

            UpdateLevelValues();
        }

        PlayerPrefs.SetInt("EXP", EXP);
        UpdateValuesOfLevel();
    }

    private void UpdateValuesOfLevel() //Обновляет значение в UI
    {
        LevelBar.fillAmount = EXP / 100;
        LevelText.text = Level.ToString();
    }

    private void CheckForRedMoonDay() 
    {
        if (PlayerPrefs.GetInt("DaysToRedMoon") == 0) //Начало красной луны
        {
            RedMoon.RedMoonStart();
            DaysText.color = Color.red;
        }

        if (PlayerPrefs.GetInt("isRedMoonDay") == 1) //Завершение красной луны
        {
            PlayerPrefs.SetInt("isRedMoonDay", 0);
            PlayerPrefs.SetInt("DaysToRedMoon", 7);
            DaysText.color = Color.white;
        }
    }
}