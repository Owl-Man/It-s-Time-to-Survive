using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private InventorySystem inventory;
	private ButtonsController buttons_controller;

	public int i;

	public bool isSlotUse = false;

	public GameObject FoodUseButton;

	public GameObject UseSlotHighLightning;

	public GameObject Child;

	public bool isSlotHaveItem = false; // for transport SlotTo

	private void Start() 
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
		buttons_controller = GameObject.FindGameObjectWithTag("ButtonsController").GetComponent<ButtonsController>();
	}

	private void Update() 
	{
		if (transform.childCount <= 0) 
		{
			inventory.isFull[i] = false;
		}
	}

	public void DropItem() 
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<Spawn>().SpawnDroppedItem();
			
			if (PlayerPrefs.GetInt("IdSlotThatUsed") == i) 
			{
				UnSelectSlot();
			}

			isSlotHaveItem = false;
			inventory.isFull[i] = false;
			GameObject.Destroy(child.gameObject);
		}
	}

	public void OnSlotUseButtonClick() 
	{
		if (isSlotUse == false && PlayerPrefs.GetInt("isAnySlotUsed") == 0) 
		{
			foreach (Transform child in transform) 
		    {
			    if (child.CompareTag("Food")) 
			    {
			      	FoodUseButton.SetActive(true);
			    }

			    child.GetComponent<Item>().isItemSelected = true;
		    }

		    SelectSlot();

		    return;
		}

		if (PlayerPrefs.GetInt("isAnySlotUsed") == 1 && PlayerPrefs.GetInt("IdSlotThatUsed") != i) 
		{
			foreach (Transform child in transform) 
			{
				isSlotHaveItem = true;
			}

			if (isSlotHaveItem == false) 
			{
				inventory.TransportItemToOtherSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), i);
			}
		}

		if (isSlotUse == true) 
		{
			foreach (Transform child in transform) 
		    {
			    if (child.CompareTag("Food")) 
			    {
			      	FoodUseButton.SetActive(false);
			    }

			    child.GetComponent<Item>().isItemSelected = false;
		    }

		    UnSelectSlot();
		}
	}

	public void OnFoodUseButtonClick() 
	{
		foreach (Transform child in transform) 
		{
			if (child.GetComponent<Item>().isItemSelected == true)
			{
				child.GetComponent<UseFood>().EatFood();
				UnSelectSlot();
		        Destroy(child.gameObject);
			}
		}
	}

	public void UnSelectSlot() 
	{
		UseSlotHighLightning.SetActive(false);
		isSlotUse = false;
		FoodUseButton.SetActive(false);
		PlayerPrefs.SetInt("isAnySlotUsed", 0);
		buttons_controller.OnBackForDescriptionPanelButtonClick();
	}

	public void SelectSlot() 
	{
		UseSlotHighLightning.SetActive(true);
		isSlotUse = true;
		PlayerPrefs.SetInt("isAnySlotUsed", 1);
		PlayerPrefs.SetInt("IdSlotThatUsed", i);
		buttons_controller.OnItemButtonClick();
	}

	public void GetChild() 
	{
		foreach (Transform child in transform) 
		{
			Child = child.gameObject;
		}
	}
}
