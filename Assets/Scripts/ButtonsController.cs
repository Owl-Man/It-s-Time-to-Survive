using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonsController : MonoBehaviour
{
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
}
