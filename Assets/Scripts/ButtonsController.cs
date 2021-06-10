using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsController : MonoBehaviour
{
	[SerializeField] private string MainPlayScene;

	public GameObject InventoryPanel;
	public GameObject DescriptionItemPanel;

	public void OnInventoryButtonClick() 
	{
		InventoryPanel.SetActive(true);
	}

	public void OnBackForInventoryButtonClick() 
	{
		InventoryPanel.SetActive(false);
	}

	public void IsOnItemButtonClick(bool state) 
	{
		DescriptionItemPanel.SetActive(state);
	}

    public void OnRestartButtonClick() => SceneManager.LoadScene(MainPlayScene);

    public void OnMenuButtonClick() => SceneManager.LoadScene("Menu");
}
