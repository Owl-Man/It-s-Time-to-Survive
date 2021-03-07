using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
	private InventorySystem inventory;
	public int i;

	private void Start() 
	{
		inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventorySystem>();
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
}
