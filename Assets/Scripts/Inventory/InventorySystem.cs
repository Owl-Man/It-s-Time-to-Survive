using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
    [Header("Other")]

    public bool[] isFull;
    public GameObject[] slots;
    public Slot[] slotScripts; //SLOT COMPONENTS CASHED IN CONTAINER

    public GameObject FoodUseButton;
    public GameObject AttackButton;

    public LinkManager links;
    public ButtonsController buttonsCntrl;

    private Weapon weapon;
    private Food food;

    private Slot slot;

    [Header("Description Fields")]

    public Text name;
    public Text description;
    public Text healOrDamage;
    public Text satietyOrRare;

    [Header("Transport Elements")]

    private GameObject SlotFromChild;
    private GameObject SlotTo;
    private GameObject SlotToChild;

    private Slot slotFromScript;
    private Slot slotToScript;

    private int IdSlotFrom;
    private int IdSlotTo;

    private void Awake() 
    {
        PlayerPrefs.SetInt("isAnySlotUsed", 0);
    }

    public void InsertDescriptionFieldsWeapon(GameObject child) //Передача описания обьекта в панель типа "оружие"
    {
        weapon = child.GetComponent<WeaponItem>().weapon;

        name.text = weapon.name;
        description.text = weapon.description;
        healOrDamage.text = "Damage: " + weapon.damage.ToString();
        satietyOrRare.text = "Rarity: " + weapon.rare;
    }

    public void InsertDescriptionFieldsFood(GameObject child) //Передача описания обьекта в панель типа "еда"
    {
        food = child.GetComponent<UseFood>().food;

        name.text = food.name;
        description.text = food.description;
        healOrDamage.text = "Heal: " + food.heal.ToString();
        satietyOrRare.text = "Satiety: " + food.satiety.ToString();
    }

    public void isSelectSlot(int id, bool state) //Global selecting or unselecting any slot
    {
        slotScripts[id].MainChangeSlotUsingState(state);
    }

    public void ChangeHaveItemState(int id, bool state)
    {
        slotScripts[id].isSlotHaveItem = state;

        isFull[id] = state;
    }

    public void AddNewItem(int i, GameObject item, GameObject obj)
    {
        isFull[i] = true;

        GameObject PickUpedItem = Instantiate(item, slots[i].transform);

        PickUpedItem.GetComponent<Spawn>().links = links;

        if (PickUpedItem.CompareTag("Food")) PickUpedItem.GetComponent<UseFood>().links = links;

        PickUpedItem.GetComponent<Item>().id = i;

        slotScripts[i].GetChild();

        slotScripts[i].isSlotHaveItem = true;

        AddItem(i, obj);
    }

    public void AddItem(int i, GameObject obj) 
    {
        ChangeItemsCount(i, 1);

        slotScripts[i].UpdateItemsCountField();

        Destroy(obj);
    }

    public void ChangeItemsCount(int id, int count) => slotScripts[id].CountOfItems += count;

    public void TransportItemToOtherSlot(int IdSlotFrom, int IdSlotTo) //Перемещение обьекта из одного слота в другой 
    {
        try
        {
            slotFromScript = slotScripts[IdSlotFrom];
            slotToScript = slotScripts[IdSlotTo];

            slotFromScript.GetChild();

            SlotFromChild = slotFromScript.Child; // Получаем ссылку на обьект в слоте, которую надо переместить

            isSelectSlot(IdSlotFrom, false);
            ChangeHaveItemState(IdSlotFrom, false);

            SlotTo = slots[IdSlotTo];

            SlotToChild = Instantiate(SlotFromChild, SlotTo.transform); // Получаем ссылку на созданный обьект в новом слоте

            int CountOfItemsSlotFrom = slotScripts[IdSlotFrom].CountOfItems;

            ChangeItemsCount(IdSlotFrom, -CountOfItemsSlotFrom);

            slotScripts[IdSlotFrom].UpdateItemsCountField();

            SlotToChild.name = SlotFromChild.name;

            SlotToChild.GetComponent<Item>().id = slotToScript.i; //Меняем id обьекта на новый, нового слота

            ChangeItemsCount(IdSlotTo, CountOfItemsSlotFrom);

            slotScripts[IdSlotTo].UpdateItemsCountField();

            isSelectSlot(IdSlotTo, true);
            ChangeHaveItemState(IdSlotTo, true);

            Destroy(SlotFromChild.gameObject); //Удаляем старый обьект в старом слоте
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            //nothing in slot, bruh moment
        }
    }
}
