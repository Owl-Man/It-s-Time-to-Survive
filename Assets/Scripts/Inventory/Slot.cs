using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private InventorySystem inventory;

	[HideInInspector]
	public PlayerController player;

	private ButtonsController buttons_controller;

	public int i;

	public bool isSlotUse = false;

	public GameObject UseSlotHighLightning;

	[HideInInspector]
	public GameObject Child;

	public bool isSlotHaveItem = false; // for transport SlotTo

	private void Start() 
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		buttons_controller = GameObject.FindGameObjectWithTag("ButtonsController").GetComponent<ButtonsController>();
	}

	public void DropItem() //Выброс предмета из инвентаря и спавн обьекта на рандомной точке, рядом с персонажем
	{
		foreach (Transform child in transform) 
		{
			child.GetComponent<Spawn>().SpawnDroppedItem();
			
			if (PlayerPrefs.GetInt("IdSlotThatUsed") == i) 
			{
				UnSelectSlot();

				if (child.CompareTag("Weapon") || child.CompareTag("Bow")) 
				{
				    player.UnBringWeapon();
				}
			}

			isSlotHaveItem = false;
			inventory.isFull[i] = false;
			GameObject.Destroy(child.gameObject);
		}
	}

	public void OnSlotUseButtonClick() 
	{
		if (isSlotUse == false && PlayerPrefs.GetInt("isAnySlotUsed") == 0) //Если слот до этого не был выделен
		{
			SlotUse();

		    return;
		}

		if (PlayerPrefs.GetInt("isAnySlotUsed") == 1 && PlayerPrefs.GetInt("IdSlotThatUsed") != i) //Если выделен другой слот
		{
			foreach (Transform child in transform) 
			{
				isSlotHaveItem = true;
			}

			if (isSlotHaveItem == true) 
			{
				inventory.UnSelectSlot(PlayerPrefs.GetInt("IdSlotThatUsed")); //Отменяет выделение слота, который был выделн
				SlotUse(); //Выделяет этот слот
			}
			else
			{
				inventory.TransportItemToOtherSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), i);
			}
		}

		if (isSlotUse == true) //Если слот был до этого выделен
		{
			foreach (Transform child in transform) 
			{
				if (child.CompareTag("Food")) 
				{
			    	inventory.FoodUseButton.SetActive(false);
				}

				if (child.CompareTag("Weapon") || child.CompareTag("Bow")) 
				{
				    inventory.AttackButton.SetActive(false);
				    player.UnBringWeapon();
				}

				child.GetComponent<Item>().isItemSelected = false;

		 	    UnSelectSlot();
			}
		}
	}

	public void SlotUse()
	{
		foreach (Transform child in transform) 
		{
			SelectSlot();

			if (child.CompareTag("Food")) 
			{
				inventory.FoodUseButton.SetActive(true);
				inventory.InsertDescriptionFieldsFood(child.gameObject); //Передача описания обьекта в панель типа "еда"
			}

			if (child.CompareTag("Weapon") || child.CompareTag("Bow")) 
			{
				inventory.AttackButton.SetActive(true);
				player.BringWeapon();
				inventory.InsertDescriptionFieldsWeapon(child.gameObject); //Передача описания обьекта в панель типа "оружие"
			}

			child.GetComponent<Item>().isItemSelected = true;
		}
	}

	public void OnFoodUseButtonClick() 
	{
		GetChild();

		if (Child.GetComponent<Item>().isItemSelected == true)
		{
			Child.GetComponent<UseFood>().EatFood();
			UnSelectSlot();
		    Destroy(Child.gameObject);
		}
	}

	public void UnSelectSlot() 
	{
		UseSlotHighLightning.SetActive(false);
		isSlotUse = false;
		inventory.FoodUseButton.SetActive(false);
		inventory.AttackButton.SetActive(true);
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

	public void GetChild() //for get and globaling child gameobject
	{
		foreach (Transform child in transform) 
		{
			Child = child.gameObject;
		}
	}

	public void CheckForFake() 
	{
		foreach (Transform child in transform) 
		{
			if (child.GetComponent<Item>().id != i) 
			{
				Destroy(child.gameObject);
			}
		}
	}
}
