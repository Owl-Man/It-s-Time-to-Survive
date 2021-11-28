using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookOfEnemyScript : MonoBehaviour
{
    [Header ("GameObjects")]
    [SerializeField] private GameObject BookPanel;
    [SerializeField] private GameObject Pockets;

    [Header ("Components")]
    [SerializeField] private Text NameEnemy, DescriptionEnemy, HealthEnemy;
    [SerializeField] private Text DamageEnemy, AttackSpeedEnemy, EXPEnemy;

    [SerializeField] private Image SpriteEnemy;

    [SerializeField] private Text CurrentPage;

    [Header ("Values")]
    [SerializeField] private int currentEnemy;

    [Header ("References")]
    [SerializeField] private Enemy[] EnemiesData;

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
