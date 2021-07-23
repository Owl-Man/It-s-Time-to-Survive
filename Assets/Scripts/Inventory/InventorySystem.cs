using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
    public GameObject FoodUseButton;
    public GameObject AttackButton;

    public ButtonsController buttonsCntrl;

    private Weapon weapon;
    private Food food;

    [Header("Description Fields")]

    [SerializeField] private Text name;
    [SerializeField] private Text description;
    [SerializeField] private Text healOrDamage;
    [SerializeField] private Text satietyOrRare;

    [Header("Transport Elements")]

    private GameObject SlotFromChild;
    private GameObject SlotTo;
    private GameObject SlotToChild;

    private Slot slotFromScript;
    private Slot slotToScript;
    
    [Header("Other")]

    public bool[] isFull;
    public GameObject[] slots;
    public Slot[] slotScripts; //SLOT COMPONENTS CASHED IN CONTAINER

    private void Awake() => PlayerPrefs.SetInt("isAnySlotUsed", 0);

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

    public void IsSelectSlot(int id, bool state) => slotScripts[id].MainChangeSlotUsingState(state);

    public void ChangeHaveItemState(int id, bool state)
    {
        slotScripts[id].isSlotHaveItem = state;

        isFull[id] = state;
    }

    public void AddNewItem(int i, GameObject item, GameObject obj)
    {
        isFull[i] = true;

        GameObject PickUpedItem = Instantiate(item, slots[i].transform);

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

            IsSelectSlot(IdSlotFrom, false);
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

            IsSelectSlot(IdSlotTo, true);
            ChangeHaveItemState(IdSlotTo, true);

            Destroy(SlotFromChild); //Удаляем старый обьект в старом слоте
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            //в слоте ничего нет
        }
    }
}
