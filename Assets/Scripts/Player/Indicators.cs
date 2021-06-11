using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Indicators : MonoBehaviour
{
    public Image[] Lives;
    public Sprite fullLive;
    public Sprite emptyLive;

    public Image[] Satiety;
    public Sprite fullSatiety;
    public Sprite emptySatiety;

    public int health;
    public int numberOfLives;

    public int satiety;
    public int numberOfSatiety;

    public float HungeringSpeed;

    public Animator animator;

    public GameObject GameOverPanel;
    public GameObject Pockets;

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
            if (i < health)
            {
                Lives[i].sprite = fullLive;
            }
            else
            {
                Lives[i].sprite = emptyLive;
            }
            if (i < numberOfLives)
            {
                Lives[i].enabled = true;
            }
            else
            {
                Lives[i].enabled = false;
            }
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
            if (i < satiety)
            {
                Satiety[i].sprite = fullSatiety;
            }
            else
            {
                Satiety[i].sprite = emptySatiety;
            }
            if (i < numberOfSatiety)
            {
                Satiety[i].enabled = true;
            }
            else
            {
                Satiety[i].enabled = false;
            }
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
