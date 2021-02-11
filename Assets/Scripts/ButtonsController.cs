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

	public void OnItemButtonClick() 
	{
		DescriptionItemPanel.SetActive(true);
	}

	public void OnBackForDescriptionPanelButtonClick() 
	{
		DescriptionItemPanel.SetActive(false);
	}
}
