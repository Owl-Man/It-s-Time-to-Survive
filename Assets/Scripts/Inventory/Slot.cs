using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Slot : MonoBehaviour
{
    private PlayerController player;
    private InventorySystem inventory;
    private ButtonsController buttons_controller;

    public int i;
    public int CountOfItems = 0;

    [Header("Slot states")]
    public bool isSlotUse = false;
    public bool isSlotChest;
    public bool isSlotHaveItem = false; // for transport SlotTo

    [Header("Other")]
    public GameObject UseSlotHighLightning;
    public LinkManager link;

    [HideInInspector] public GameObject Child;

    public Text ItemsCountField;


    private void Start()
    {
        player = link.playerController;

        inventory = player.inventory;

        buttons_controller = inventory.buttonsCntrl;

        UpdateItemsCountField();
    }

    public void UpdateItemsCountField() => ItemsCountField.text = CountOfItems.ToString();

    public void DropItem() //Выброс предмета из инвентаря и спавн обьекта на рандомной точке, рядом с персонажем
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();

            if (PlayerPrefs.GetInt("IdSlotThatUsed") == i) MainChangeSlotUsingState(false);

            isSlotHaveItem = false;
            inventory.isFull[i] = false;

            RemoveItems(1);
        }
    }

    void OnSlotUseButtonClick()  //Нажатие на кнопку слота
    {
        if (isSlotUse == false && PlayerPrefs.GetInt("isAnySlotUsed") == 0) //Если слот до этого не был выделен
        {
            MainChangeSlotUsingState(true);

            return;
        }
        else if (isSlotUse == true)
        {
            MainChangeSlotUsingState(false);
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
                MainChangeSlotUsingState(true); //Выделяет этот слот
            }
            else
            {
                inventory.TransportItemToOtherSlot(PlayerPrefs.GetInt("IdSlotThatUsed"), i);
            }
        }
    }

    public void MainChangeSlotUsingState(bool state) // <-----<-----<----<-- MAIN SYSTEM CHANGER SLOT USING STATE
    {
        GetChild();

        isSelectSlot(state);

        if (Child == null) return;

        BringItemState(state);

        Child.GetComponent<Item>().isItemSelected = state;
    }

    public void BringItemState(bool state)
    {
        if (isSlotChest == true) state = false;

        GetChild();

        if (Child.CompareTag("Food"))
        {
            inventory.FoodUseButton.SetActive(state);

            if (state == true) inventory.InsertDescriptionFieldsFood(Child.gameObject); //Передача описания обьекта в панель типа "еда"
        }

        if (Child.CompareTag("Weapon") || Child.CompareTag("Bow"))
        {
            inventory.AttackButton.SetActive(state);
            player.BringWeaponState(state);

            if (state == true) inventory.InsertDescriptionFieldsWeapon(Child.gameObject); //Передача описания обьекта в панель типа "оружие"
        }
    }
    private void isSelectSlot(bool state) //Выделение слота
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

    public void OnFoodUseButtonClick()
    {
        GetChild();

        if (Child != null && Child.GetComponent<Item>().isItemSelected == true)
        {
            MainChangeSlotUsingState(false);

            Child.GetComponent<UseFood>().EatFood();

            RemoveItems(1);
        }
    }

    private void RemoveItems(int count)
    {
        CountOfItems -= count;
        UpdateItemsCountField();

        foreach (Transform child in transform)
        {
            if (CountOfItems == 0)
            {
                GameObject.Destroy(child.gameObject);
            }
        }

    }


    public void GetChild() //for get and globaling child gameobject
    {
        foreach (Transform child in transform)
        {
            Child = child.gameObject;
        }
    }
}