using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
	public bool[] isFull;
	public GameObject[] slots;

//<------TRANSPORT ELEMENTS------>

	private GameObject SlotFrom;
	private GameObject SlotTo;
	private GameObject SlotToChild;

	private Slot slotFromScript;
	private Slot slotToScript;

	public void TransportItemToOtherSlot(int IdSlotFrom, int IdSlotTo) //Перемещение обьекта из одного слота в другой
	{
		try 
		{
			slotFromScript = slots[IdSlotFrom].GetComponent<Slot>();
			slotToScript = slots[IdSlotTo].GetComponent<Slot>();

			slotFromScript.GetChild();
			SlotFrom = slotFromScript.Child; // Получаем ссылку на обьект в слоте, которую надо переместить

			slotFromScript.UnSelectSlot();
			slotFromScript.isSlotHaveItem = false;

			isFull[IdSlotFrom] = false;

		    SlotTo = slots[IdSlotTo];

    	    SlotToChild = Instantiate(SlotFrom, SlotTo.transform); // Получаем ссылку на созданный обьект в новом слоте

    	    SlotToChild.GetComponent<Item>().id = slotToScript.i; //Меняем id обьекта на новый, нового слота

    	    slotToScript.isSlotHaveItem = true;

    	    isFull[IdSlotTo] = true;

    	    Destroy(SlotFrom.gameObject); //Удаляем старый обьект в старом слоте
		}
		catch(Exception ex) 
		{
			//nothing in slot, bruh moment
		}
	}
}
