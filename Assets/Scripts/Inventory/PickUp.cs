using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
	private InventorySystem inventory;
	public GameObject slotButton; // item in slot

	private void Start() 
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
	}

	private void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.CompareTag("Player")) 
		{
			for (int i = 0; i < inventory.slots.Length; i++)
			{
				if (inventory.isFull[i] == false) 
				{
					inventory.isFull[i] = true;

					GameObject PickUpedItem = Instantiate(slotButton, inventory.slots[i].transform);
					
					PickUpedItem.GetComponent<Item>().id = i;

					Destroy(gameObject);
					break;
				}
			}
		}
	}
}
