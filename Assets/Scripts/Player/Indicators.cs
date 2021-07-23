using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    [SerializeField] private Image[] Lives;
    [SerializeField] private Sprite fullLive;
    [SerializeField] private Sprite emptyLive;

    [SerializeField] private Image[] Satiety;
    [SerializeField] private Sprite fullSatiety;
    [SerializeField] private Sprite emptySatiety;

    [SerializeField] private float HungeringSpeed;

    [SerializeField] private Animator animator;

    [SerializeField] private GameObject GameOverPanel;
    [SerializeField] private GameObject Pockets;

    public int health;
    [SerializeField] private int numberOfLives;

    public int satiety;
    [SerializeField] private int numberOfSatiety;


    private bool isSatietyDying;

    private void Start() 
    {
        UpdateAllValues();
        StartCoroutine(Hungering());
    }

    public void UpdateAllValues()
    {
        HealthDiagnostic();
        SatietyDiagnostic();
    }

    public void HealthDiagnostic() 
    {
        if (health > numberOfLives)
        {
            health = numberOfLives;
        }

        for (int i = 0; i < Lives.Length; i++)
        {
            Lives[i].sprite = i < health ? fullLive : emptyLive;
            Lives[i].enabled = i < numberOfLives;
        }

        if (health <= 0)
        {
            StartCoroutine(DiePlayer());
        }

        if (health < 0) 
        {
            health = 0;
        }
    }

    public void SatietyDiagnostic() 
    {
        if (satiety > numberOfSatiety) 
        {
            satiety = numberOfSatiety;
        }

        for (int i = 0; i < Satiety.Length; i++)
        {
            Satiety[i].sprite = i < satiety ? fullSatiety : emptySatiety;
            Satiety[i].enabled = i < numberOfSatiety;
        }

        if (satiety <= 0 && isSatietyDying == false) 
        {
            isSatietyDying = true;
            StartCoroutine(SatietyDying());
        }
        else if (satiety > 0)
        {
            isSatietyDying = false;
            StopCoroutine(SatietyDying());
        }

        if (satiety < 0) 
        {
            satiety = 0;
        }
    }

    IEnumerator SatietyDying()
    {
        yield return new WaitForSeconds(3.5f);
        health--;
        HealthDiagnostic();
        StartCoroutine(SatietyDying());
    }

    IEnumerator Hungering() 
    {
        yield return new WaitForSeconds(HungeringSpeed);
        satiety--;
        SatietyDiagnostic();
        StartCoroutine(Hungering());
    }

    IEnumerator DiePlayer() 
    {
        animator.SetBool("isDead", true);
        yield return new WaitForSeconds(1);
        GameOverPanel.SetActive(true);
        Pockets.SetActive(false);
        gameObject.SetActive(false);
    }
}
