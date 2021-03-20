using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private InventorySystem inventory;

	public int i;

	public bool isSlotUse;

	public GameObject FoodUseButton;

	public GameObject UseSlotHighLightning;

	public GameObject Child;

	private void Start() 
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();

		isSlotUse = false;
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
			UnSelectSlot();
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
			inventory.TransportItemToOtherSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), i);
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
	}

	public void SelectSlot() 
	{
		UseSlotHighLightning.SetActive(true);
		isSlotUse = true;
		PlayerPrefs.SetInt("isAnySlotUsed", 1);
		PlayerPrefs.SetInt("IdSlotThatUsed", i);
	}

	public void GetChild() 
	{
		foreach (Transform child in transform) 
		{
			Child = child.gameObject;
		}
	}
}
