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
			GameObject.Destroy(child.gameObject);
		}
	}

	public void OnSlotUseButtonClick() 
	{
		if (isSlotUse == false) 
		{
			UseSlotHighLightning.SetActive(true);

			foreach (Transform child in transform) 
		    {
			    if (child.CompareTag("Food")) 
			    {
			      	FoodUseButton.SetActive(true);
			    }

			    child.GetComponent<Item>().id = -1;
		    }

		    isSlotUse = true;

		    return;
		}

		if (isSlotUse == true) 
		{
			UseSlotHighLightning.SetActive(false);

			foreach (Transform child in transform) 
		    {
			    if (child.CompareTag("Food")) 
			    {
			      	FoodUseButton.SetActive(false);
			    }

			    child.GetComponent<Item>().id = i;
		    }

		    isSlotUse = false;
		}
	}

	public void OnFoodUseButtonClick() 
	{
		foreach (Transform child in transform) 
		{
			if (child.GetComponent<Item>().id == -1) 
			{
				child.GetComponent<UseFood>().EatFood();
				UseSlotHighLightning.SetActive(false);
		        isSlotUse = false;
		        FoodUseButton.SetActive(false);

		        Destroy(child.gameObject);
			}
		}
	}
}
