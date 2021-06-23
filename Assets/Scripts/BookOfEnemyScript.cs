using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookOfEnemyScript : MonoBehaviour
{
    [Header ("GameObjects")]
    public GameObject BookPanel;
    public GameObject Pockets;

    [Header ("Components")]
    public Text NameEnemy;
    public Text DescriptionEnemy;
    public Text DamageEnemy;
    public Text HealthEnemy;
    public Text AttackSpeedEnemy;
    public Text EXPEnemy;

    public Image SpriteEnemy;

    public Text CurrentPage;

    [Header ("Values")]
    public int currentEnemy = 0;

    [Header ("References")]
    public Enemy[] EnemiesData;

    public void OnBookOfEvilButtonClick() => SetBookPanelActive(true);

    public void OnBackForBookOfEvilButtonClick() => SetBookPanelActive(false);

    public void OnLeftArrowButtonClick() => ChangeCurrentEnemy(-1);

    public void OnRightArrowButtonClick() => ChangeCurrentEnemy(1);

    private void ChangeCurrentEnemy(int count) 
    {
        currentEnemy += count;

        if (currentEnemy < 0) 
        {
            currentEnemy = 0;
            return;
        }

        if (currentEnemy >= EnemiesData.Length) 
        {
            currentEnemy = EnemiesData.Length - 1;
            return;
        }

        UpdateFields();
    }

    private void SetBookPanelActive(bool state) 
    {
        BookPanel.SetActive(state);
        
        if(Pockets != null) Pockets.SetActive(!state);

        if (state == true) UpdateFields();
    }

    private void UpdateFields() 
    {
        NameEnemy.text = EnemiesData[currentEnemy].name;
        DescriptionEnemy.text = EnemiesData[currentEnemy].description;
        DamageEnemy.text = "Damage " + EnemiesData[currentEnemy].damage.ToString();
        HealthEnemy.text = "Health " + EnemiesData[currentEnemy].health.ToString();
        AttackSpeedEnemy.text = "Attack Speed " + EnemiesData[currentEnemy].attackSpeed.ToString();
        EXPEnemy.text = "EXP " + EnemiesData[currentEnemy].EXP.ToString();

        SpriteEnemy.sprite = EnemiesData[currentEnemy].sprite;

        CurrentPage.text = (currentEnemy + 1).ToString();
    }
}
