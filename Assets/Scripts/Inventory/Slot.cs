using Player;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private PlayerController _player;
    private InventorySystem _inventory;
    private ButtonsController _buttonsController;

    public int i;
    public int CountOfItems;

    [Header("Slot states")]

    public bool isSlotUse;
    public bool isSlotChest;
    public bool isSlotHaveItem; // for transport SlotTo

    [Header("Other")]
    
    public GameObject UseSlotHighLightning;

    [HideInInspector] public GameObject Child;

    [SerializeField] private Text ItemsCountField;


    private void Start()
    {
        _player = LinkManager.instance.playerController;

        _inventory = LinkManager.instance.inventory;

        _buttonsController = _inventory.buttonsCntrl;

        UpdateItemsCountField();
    }

    public void UpdateItemsCountField() => ItemsCountField.text = CountOfItems.ToString();

    public void DropItem() //Выброс предмета из инвентаря и спавн обьекта на рандомной точке, рядом с персонажем
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnDroppedItem();

            if (_inventory.idSlotThatUsed == i) MainChangeSlotUsingState(false);

            isSlotHaveItem = false;
            _inventory.isFull[i] = false;

            RemoveItems(1);
        }
    }

    public void OnSlotUseButtonClick()  //Нажатие на кнопку слота
    {
        if (!isSlotUse && !_inventory.isAnySlotUsed) //Если слот до этого не был выделен
        {
            MainChangeSlotUsingState(true);
            return;
        }
        else if (isSlotUse)
        {
            MainChangeSlotUsingState(false);
        }

        if (_inventory.isAnySlotUsed && _inventory.idSlotThatUsed != i) //Если выделен другой слот
        {
            GetChild();

            if (Child != null) isSlotHaveItem = true;

            else isSlotHaveItem = false;

            if (isSlotHaveItem)
            {
                _inventory.IsSelectSlot(_inventory.idSlotThatUsed, false); //Отменяет выделение слота, который был выделен
                MainChangeSlotUsingState(true); //Выделяет этот слот
            }
            else
            {
                _inventory.TransportItemToOtherSlot(_inventory.idSlotThatUsed, i);
            }
        }
    }

    public void MainChangeSlotUsingState(bool state) // <-----<-----<----<-- MAIN SYSTEM CHANGER SLOT USING STATE
    {
        GetChild();

        IsSelectSlot(state);

        if (Child == null) return;

        BringItemState(state);

        Child.GetComponent<Item>().isItemSelected = state;
    }

    public void BringItemState(bool state)
    {
        if (isSlotChest) state = false;

        GetChild();

        if (Child.CompareTag("Food"))
        {
            _inventory.FoodUseButton.SetActive(state);

            if (state) 
                _inventory.InsertDescriptionFieldsFood(Child.gameObject); //Передача описания обьекта в панель типа "еда"
        }

        if (Child.CompareTag("Weapon") || Child.CompareTag("Bow"))
        {
            _inventory.AttackButton.SetActive(state);
            _player.BringWeaponState(state);

            if (state) 
                _inventory.InsertDescriptionFieldsWeapon(Child.gameObject); //Передача описания обьекта в панель типа "оружие"
        }
    }
    private void IsSelectSlot(bool state) //Выделение слота
    {
        UseSlotHighLightning.SetActive(state);
        isSlotUse = state;

        if (state)
        {
            _inventory.isAnySlotUsed = true;
            _inventory.idSlotThatUsed = i;
        }
        else
        {
            _inventory.isAnySlotUsed = false;
            _inventory.FoodUseButton.SetActive(false);
            _inventory.AttackButton.SetActive(false);
        }

        _buttonsController.IsOnItemButtonClick(state);
    }

    public void OnFoodUseButtonClick()
    {
        foreach (Transform child in transform)
        {
            if (child.GetComponent<Item>().isItemSelected)
            {
                MainChangeSlotUsingState(false);

                child.GetComponent<UseFood>().EatFood();

                RemoveItems(1);
            }
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
                _inventory.isFull[i] = false;
                Destroy(child.gameObject);
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