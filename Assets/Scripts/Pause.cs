using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject PausePanel;

    public GameObject Pockets;

    private bool isPausePanelActivate;

    public void OnPauseButtonClick() 
    {
        PausePanel.SetActive(true);

        if (Pockets != null) Pockets.SetActive(false);

        StartCoroutine(TimeChange());
    }

    IEnumerator TimeChange() 
    {
        yield return new WaitForSeconds(0.52f);
        Time.timeScale = 0f;
        isPausePanelActivate = true;
    }

    public void OnBackForPauseButtonClick() 
    {
        if (isPausePanelActivate == false) return;

        Time.timeScale = 1f;
        PausePanel.SetActive(false);

        isPausePanelActivate = false;

        if (Pockets != null) Pockets.SetActive(true);
    }
}
