using System.Collections;
using UnityEngine;

public class Pause : MonoBehaviour
{
    [SerializeField] private GameObject PausePanel;

    [SerializeField] private GameObject Pockets;

    private bool _isPausePanelActivate;

    private void Start() => Pockets.SetActive(true);

    public void OnPauseButtonClick() 
    {
        PausePanel.SetActive(true);

        if (Pockets != null) Pockets.SetActive(false);

        StartCoroutine(TimeChange());
    }

    private IEnumerator TimeChange() 
    {
        yield return new WaitForSeconds(0.52f);
        Time.timeScale = 0f;
        _isPausePanelActivate = true;
    }

    public void OnBackForPauseButtonClick() 
    {
        if (_isPausePanelActivate == false) return;

        Time.timeScale = 1f;
        PausePanel.SetActive(false);

        _isPausePanelActivate = false;

        if (Pockets != null) Pockets.SetActive(true);
    }
}
