using Instruments;
using Inventory;
using UnityEngine;

public class Chest : MonoBehaviour
{
	[SerializeField] private GameObject ChestPanel;

	[SerializeField] private GameObject[] ChestContainer;
	
	private InventorySystem _inventory;
	
	private void Start() => _inventory = LinkManager.Instance.inventory;

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

		while (i < _inventory.slots.Length && a < ChestContainer.Length)
		{
			if (_inventory.isFull[i] == false && ChestContainer[a] != null) 
			{
				_inventory.isFull[i] = true;

				GameObject swappedItem = Instantiate(ChestContainer[a], _inventory.slots[i].transform);

				_inventory.slotScripts[i].GetChild();
					
				swappedItem.GetComponent<Item>().id = i;

				Slot slot = _inventory.slotScripts[i];

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

		while (i < _inventory.slots.Length && a < ChestContainer.Length)
		{
			if (_inventory.isFull[i] == true) 
			{
				_inventory.isFull[i] = false;

				ChestContainer[a] = _inventory.slots[i];

				Slot slot = _inventory.slotScripts[i];

				slot.isSlotHaveItem = false;

				slot.GetChild();
				Destroy(slot.Child.gameObject);
			}

			i++;
			a++;
		}
	}
}
