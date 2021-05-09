using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Chest : LinkManager
{
	private InventorySystem inventory;
	public GameObject ChestPanel;

	public GameObject[] ChestContainer;

	private void Start() => inventory = ManagerInventory;

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Player")) 
		{
			ChestPanel.SetActive(true);
			SwapItems();
		}
	}

	private void OnTriggerExit2D(Collider2D other) 
	{
		if (other.CompareTag("Player")) 
		{
			ChestPanel.SetActive(false);
			BackSwapItems();
		}
	}

	private void SwapItems() //Перемещение items из контейнера сундука в слоты панели сундука
	{
		int i = 15;
		int a = 0;

		while (i < inventory.slots.Length && a < ChestContainer.Length)
		{
			if (inventory.isFull[i] == false && ChestContainer[a] != null) 
			{
				inventory.isFull[i] = true;

				GameObject SwapedItem = Instantiate(ChestContainer[a], inventory.slots[i].transform);

				inventory.slotScripts[i].GetChild();
					
				SwapedItem.GetComponent<Item>().id = i;

				Slot slot = inventory.slotScripts[i];

				slot.isSlotHaveItem = true;
			}

			i++;
			a++;
		}

		for (int d = 0; d < ChestContainer.Length; d++) //Опустошение контейнера сундука
		{
			ChestContainer[d] = null;
		}
	}

	private void BackSwapItems() //Обратное действие. Сохранение изменений в контейнер, путем считывания обьектов из слотов панели
	{
		int i = 15;
		int a = 0;

		while (i < inventory.slots.Length && a < ChestContainer.Length)
		{
			if (inventory.isFull[i] == true) 
			{
				inventory.isFull[i] = false;

				ChestContainer[a] = inventory.slots[i];

				Slot slot = inventory.slotScripts[i];

				slot.isSlotHaveItem = false;

				slot.GetChild();
				Destroy(slot.Child.gameObject);
			}

			i++;
			a++;
		}
	}
}
