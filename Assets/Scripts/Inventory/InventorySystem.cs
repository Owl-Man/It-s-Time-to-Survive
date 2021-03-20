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

	public void TransportItemToOtherSlot(int IdSlotFrom, int IdSlotTo) //Перемещение обьекта из одного слота в другой
	{
		try 
		{
			slots[IdSlotFrom].GetComponent<Slot>().GetChild();
			SlotFrom = slots[IdSlotFrom].GetComponent<Slot>().Child; // Получаем ссылку на обьект в слоте, которую надо переместить

			slots[IdSlotFrom].GetComponent<Slot>().UnSelectSlot();

		    SlotTo = slots[IdSlotTo].gameObject;

    	    Instantiate(SlotFrom, SlotTo.transform);

    	    SlotTo.GetComponent<Slot>().GetChild();

    	    SlotToChild = SlotTo.GetComponent<Slot>().Child;// Получаем ссылку на созданный обьект в новом слоте

    	    SlotToChild.GetComponent<Item>().id = SlotTo.GetComponent<Slot>().i; //Меняем id обьекта на новый, нового слота

    	    Destroy(SlotFrom.gameObject); //Удаляем старый обьект в старом слоте
		}
		catch(Exception ex) 
		{
			//nothing in slot, bruh moment
		}
	}
}
