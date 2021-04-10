using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
	public bool[] isFull;
	public GameObject[] slots;

	public GameObject FoodUseButton;
	public GameObject AttackButton;

	public ButtonsController buttonsCntrl;

	private Weapon weapon;
	private Food food;

//<------DescriptionFields------>

	public Text name;
	public Text description;
	public Text healOrDamage;
	public Text satiety;

//<------TRANSPORT ELEMENTS------>

	private GameObject SlotFromChild;
	private GameObject SlotFromChildOld;
	private GameObject SlotTo;
	private GameObject SlotToChild;
	private GameObject SlotToChildOld;

	private Slot slotFromScript;
	private Slot slotToScript;

	public void InsertDescriptionFieldsWeapon(GameObject child)
	{
		weapon = child.GetComponent<WeaponItem>().weapon;

		name.text = weapon.name;
		description.text = weapon.description;
		healOrDamage.text = "Damage: " + weapon.damage.ToString();
		satiety.text = "";
	}

	public void InsertDescriptionFieldsFood(GameObject child)
	{
		food = child.GetComponent<UseFood>().food;

		name.text = food.name;
		description.text = food.description;
		healOrDamage.text = "Heal: " + food.heal.ToString();
		satiety.text = "Satiety: " + food.satiety.ToString();
	}

	public void TransportItemToOtherSlot(int IdSlotFrom, int IdSlotTo) //Перемещение обьекта из одного слота в другой
	{
		try 
		{
			slotFromScript = slots[IdSlotFrom].GetComponent<Slot>();
			slotToScript = slots[IdSlotTo].GetComponent<Slot>();

			slotFromScript.GetChild();
			SlotFromChild = slotFromScript.Child; // Получаем ссылку на обьект в слоте, которую надо переместить

			slotFromScript.UnSelectSlot();
			slotFromScript.isSlotHaveItem = false;

			isFull[IdSlotFrom] = false;

		    SlotTo = slots[IdSlotTo];

    	    SlotToChild = Instantiate(SlotFromChild, SlotTo.transform); // Получаем ссылку на созданный обьект в новом слоте

    	    SlotToChild.name = SlotFromChild.name;

    	    SlotToChild.GetComponent<Item>().id = slotToScript.i; //Меняем id обьекта на новый, нового слота

    	    slotToScript.isSlotHaveItem = true;

    	    isFull[IdSlotTo] = true;

    	    Destroy(SlotFromChild.gameObject); //Удаляем старый обьект в старом слоте
		}
		catch(Exception ex) 
		{
			//nothing in slot, bruh moment
		}
	}

	public void TransportItemToOtherSlotBoth(int IdSlotFrom, int IdSlotTo) //Перемещение двух обьектов между двумя слотами
	{
		try 
		{
			slotFromScript = slots[IdSlotFrom].GetComponent<Slot>();
			slotToScript = slots[IdSlotTo].GetComponent<Slot>();

			slotFromScript.GetChild();
			SlotFromChildOld = slotFromScript.Child; // Получаем ссылку на обьект в слоте, которую надо переместить

			slotToScript.GetChild();
			SlotToChildOld = slotToScript.Child;

			slotFromScript.UnSelectSlot();

		    SlotTo = slots[IdSlotTo];

    	    SlotToChild = Instantiate(SlotFromChildOld, SlotTo.transform); // Получаем ссылку на созданный обьект в новом слоте
    	    SlotFromChild = Instantiate(SlotToChildOld, SlotTo.transform); // Получаем ссылку на созданный обьект в новом слоте

    	    SlotToChild.name = SlotFromChildOld.name;
    	    SlotFromChild.name = SlotToChildOld.name;

    	    SlotToChild.GetComponent<Item>().id = slotToScript.i; //Меняем id обьекта на новый, нового слота
    	    SlotFromChild.GetComponent<Item>().id = slotFromScript.i; //Меняем id обьекта на новый, нового слота

    	    Destroy(SlotFromChild.gameObject); //Удаляем старый обьект в старом слоте
    	    Destroy(SlotToChild.gameObject); //Удаляем старый обьект в старом слоте
		}
		catch (Exception ex) 
		{
			//nothing in slot, bruh moment
		}
	}
}
