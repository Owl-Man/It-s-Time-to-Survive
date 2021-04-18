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
				ChangeSlotUsingState(false);

				if (child.CompareTag("Weapon") || child.CompareTag("Bow")) 
				{
				    player.BringWeaponState(false);
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
			ChangeSlotUsingState(true);

		    return;
		}
		else if (isSlotUse == true)
		{
			ChangeSlotUsingState(false);
		}

		if (PlayerPrefs.GetInt("isAnySlotUsed") == 1 && PlayerPrefs.GetInt("IdSlotThatUsed") != i) //Если выделен другой слот
		{
			foreach (Transform child in transform) 
			{
				isSlotHaveItem = true;
			}

			if (isSlotHaveItem == true) 
			{
				inventory.isSelectSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), false); //Отменяет выделение слота, который был выделен
				ChangeSlotUsingState(true); //Выделяет этот слот
			}
			else
			{
				inventory.TransportItemToOtherSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), i);
			}
		}
	}

	public void ChangeSlotUsingState(bool state)
	{
		foreach (Transform child in transform) 
		{
			isSelectSlot(state);

			if (child.CompareTag("Food")) 
			{
				inventory.FoodUseButton.SetActive(state);

				if (state == true) inventory.InsertDescriptionFieldsFood(child.gameObject); //Передача описания обьекта в панель типа "еда"
			}

			if (child.CompareTag("Weapon") || child.CompareTag("Bow")) 
			{
				inventory.AttackButton.SetActive(state);
				player.BringWeaponState(state);

				if (state == true) inventory.InsertDescriptionFieldsWeapon(child.gameObject); //Передача описания обьекта в панель типа "оружие"
			}

			child.GetComponent<Item>().isItemSelected = state;
		}
	}

	public void OnFoodUseButtonClick() 
	{
		GetChild();

		if (Child.GetComponent<Item>().isItemSelected == true)
		{
			Child.GetComponent<UseFood>().EatFood();
			ChangeSlotUsingState(false);
		    Destroy(Child.gameObject);
		}
	}

	public void isSelectSlot(bool state) 
	{
		UseSlotHighLightning.SetActive(state);
		isSlotUse = state;

		if (state == true)
		{
			PlayerPrefs.SetInt("isAnySlotUsed", 1);
			PlayerPrefs.SetInt("IdSlotThatUsed", i);
		}
		else 
		{
			PlayerPrefs.SetInt("isAnySlotUsed", 0);
			inventory.FoodUseButton.SetActive(false);
			inventory.AttackButton.SetActive(false);
		}

		buttons_controller.IsOnItemButtonClick(state);
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
