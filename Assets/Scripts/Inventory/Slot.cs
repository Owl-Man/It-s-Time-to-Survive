using UnityEngine;

public class Slot : LinkManager
{
    private InventorySystem inventory;

    [HideInInspector]
    public PlayerController player;

    private ButtonsController buttons_controller;

    public int i;

    public int CountOfItems = 0;

    public bool isSlotUse = false;
    public bool isSlotChest;

    public GameObject UseSlotHighLightning;

    [HideInInspector]
    public GameObject Child;

    public bool isSlotHaveItem = false; // for transport SlotTo

    private void Start()
    {
        player = ManagerPlayerCntrl;

        inventory = ManagerInventory;

        buttons_controller = inventory.buttonsCntrl;
    }

    public void DropItem() //Выброс предмета из инвентаря и спавн обьекта на рандомной точке, рядом с персонажем
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();

            if (PlayerPrefs.GetInt("IdSlotThatUsed") == i)
            {
                MainChangeSlotUsingState(false);

                if (child.CompareTag("Weapon") || child.CompareTag("Bow"))
                {
                    player.BringWeaponState(false);
                }
            }

            isSlotHaveItem = false;
            inventory.isFull[i] = false;
            GameObject.Destroy(child.gameObject);
        }
    }

    public void OnSlotUseButtonClick()  //Нажатие на кнопку слота
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

    public void MainChangeSlotUsingState(bool state) /* <-----<-----<----<-- MAIN CHANGER SLOT USING STATE -<-----<-----<----<----<*/
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

    public void OnFoodUseButtonClick()
    {
        GetChild();

        if (Child.GetComponent<Item>().isItemSelected == true)
        {
            Child.GetComponent<UseFood>().EatFood();
            MainChangeSlotUsingState(false);
            Destroy(Child.gameObject);
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

    public void GetChild() //for get and globaling child gameobject
    {
        foreach (Transform child in transform)
        {
            Child = child.gameObject;
        }
    }
}