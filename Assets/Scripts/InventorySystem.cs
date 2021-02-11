using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{
	[Header ("ScriptableObjects")]
	public Food apple;
	public Food banana;

	public Image[] SlotSprites;

	public static bool[] isSlotBusy = new bool[6];
	public static string[] ItemInSlot = new string[6];
	public static int[] CountOfItemsInSlot = new int[6];

	static int i = 0;

	public void Start() 
	{
		UpdateInventoryUI();
	}

	public static void AddItem(string item) 
	{
		InventorySystem inventory = new InventorySystem();

		i = 0;

		//Прокручивание массива слотов, до ближайщего доступного для вставление предмета

		while (isSlotBusy[i] == true && ItemInSlot[i] != item && i < 7)
		{
			if (isSlotBusy[i] == false || ItemInSlot[i] == item)
		    {
			    if (ItemInSlot[i] == item) 
			    {
	                CountOfItemsInSlot[i]++;
			    }
			    else if (isSlotBusy[i] == false) 
			    {
			    	int bruh = i + 1;
			    	ItemInSlot[bruh] = item;
			    	isSlotBusy[i] = true;
			    }

			    inventory.UpdateInventoryUI();
		    }

		    i++;
		}

		if (isSlotBusy[0] == false || ItemInSlot[0] == item) 
		{
			if(ItemInSlot[0] == item) 
			{
				CountOfItemsInSlot[0]++;

				Debug.Log("added item to simple item and now count is " + CountOfItemsInSlot[0]);
			}
			else if (isSlotBusy[0] == false) 
			{
				ItemInSlot[0] = item;
				isSlotBusy[0] = true;

				Debug.Log("aded item " + ItemInSlot[0]);
			}
		}
	}

	public void UpdateInventoryUI() 
	{
		Debug.Log("Updated");
		
		if (ItemInSlot[0] == "Apple")
		{
			SlotSprites[0].sprite = apple.sprite;
		}

		if (ItemInSlot[0] == "Banana") 
		{
			SlotSprites[0].sprite = banana.sprite;
		}

		if (ItemInSlot[1] == "Apple")
		{
			SlotSprites[1].sprite = apple.sprite;
		}

		if (ItemInSlot[1] == "Banana") 
		{
			SlotSprites[1].sprite = banana.sprite;
		}

		if (ItemInSlot[2] == "Apple")
		{
			SlotSprites[2].sprite = apple.sprite;
		}

		if (ItemInSlot[2] == "Banana") 
		{
			SlotSprites[2].sprite = banana.sprite;
		}

		if (ItemInSlot[3] == "Apple")
		{
			SlotSprites[3].sprite = apple.sprite;
		}

		if (ItemInSlot[3] == "Banana") 
		{
			SlotSprites[3].sprite = banana.sprite;
		}

		if (ItemInSlot[4] == "Apple")
		{
			SlotSprites[4].sprite = apple.sprite;
		}

		if (ItemInSlot[4] == "Banana") 
		{
			SlotSprites[4].sprite = banana.sprite;
		}

		if (ItemInSlot[5] == "Apple")
		{
			SlotSprites[5].sprite = apple.sprite;
		}

		if (ItemInSlot[5] == "Banana") 
		{
			SlotSprites[5].sprite = banana.sprite;
		}
	}
}
