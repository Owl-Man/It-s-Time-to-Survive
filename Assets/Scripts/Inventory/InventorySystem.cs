using UnityEngine;
using UnityEngine.UI;
using System;

public class InventorySystem : MonoBehaviour
{
    [Header("Components")]
    public GameObject FoodUseButton;
    public GameObject AttackButton;

    public ButtonsController buttonsCntrl;

    private Weapon _weapon;
    private Food _food;

    [Header("Description Fields")]

    [SerializeField] private Text name;
    [SerializeField] private Text description;
    [SerializeField] private Text healOrDamage;
    [SerializeField] private Text satietyOrRare;

    [Header("Transport Elements")]

    private GameObject _slotFromChild;
    private GameObject _slotTo;
    private GameObject _slotToChild;

    private Slot _slotFromScript;
    private Slot _slotToScript;
    
    [Header("Other")]

    public bool[] isFull;
    public GameObject[] slots;
    public Slot[] slotScripts; //Список закешированных ссылок на компоненты слотов

    private void Awake() => PlayerPrefs.SetInt("isAnySlotUsed", 0);

    public void InsertDescriptionFieldsWeapon(GameObject child) //Передача описания обьекта в панель типа "оружие"
    {
        _weapon = child.GetComponent<WeaponItem>().weapon;

        name.text = _weapon.name;
        description.text = _weapon.description;
        healOrDamage.text = "Damage: " + _weapon.damage.ToString();
        satietyOrRare.text = "Rarity: " + _weapon.rare;
    }

    public void InsertDescriptionFieldsFood(GameObject child) //Передача описания обьекта в панель типа "еда"
    {
        _food = child.GetComponent<UseFood>().food;

        name.text = _food.name;
        description.text = _food.description;
        healOrDamage.text = "Heal: " + _food.heal.ToString();
        satietyOrRare.text = "Satiety: " + _food.satiety.ToString();
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

        GameObject pickUppedItem = Instantiate(item, slots[i].transform);

        pickUppedItem.GetComponent<Item>().id = i;

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

    public void TransportItemToOtherSlot(int idSlotFrom, int idSlotTo) //Перемещение обьекта из одного слота в другой 
    {
        try
        {
            _slotFromScript = slotScripts[idSlotFrom];
            _slotToScript = slotScripts[idSlotTo];

            _slotFromScript.GetChild();

            _slotFromChild = _slotFromScript.Child; // Получаем ссылку на обьект в слоте, которую надо переместить

            IsSelectSlot(idSlotFrom, false);
            ChangeHaveItemState(idSlotFrom, false);

            _slotTo = slots[idSlotTo];

            _slotToChild = Instantiate(_slotFromChild, _slotTo.transform); // Получаем ссылку на созданный обьект в новом слоте

            int countOfItemsSlotFrom = slotScripts[idSlotFrom].CountOfItems;

            ChangeItemsCount(idSlotFrom, -countOfItemsSlotFrom);

            slotScripts[idSlotFrom].UpdateItemsCountField();

            _slotToChild.name = _slotFromChild.name;

            _slotToChild.GetComponent<Item>().id = _slotToScript.i; //Меняем id обьекта на новый, нового слота

            ChangeItemsCount(idSlotTo, countOfItemsSlotFrom);

            slotScripts[idSlotTo].UpdateItemsCountField();

            IsSelectSlot(idSlotTo, true);
            ChangeHaveItemState(idSlotTo, true);

            Destroy(_slotFromChild); //Удаляем старый обьект в старом слоте
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
            //в слоте ничего нет
        }
    }
}
