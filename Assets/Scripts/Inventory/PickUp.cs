using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] private GameObject slotButton; // item in slot
    
    private InventorySystem _inventory;
    
    private GameObject _child;

    private Item _item;

    private bool _isPickedUp;

    private void Start() => _inventory = LinkManager.instance.inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isPickedUp == false && other.CompareTag("Player"))
        {
            _isPickedUp = true;

            //Прокрутка для нахождения уже существующего предмета такого же типа и не превыщающим макс кол-во в одном слоте

            for (int a = 0; a < _inventory.slots.Length; a++)
            {
                _inventory.slotScripts[a].GetChild();
                _child = _inventory.slotScripts[a].Child;

                if (_child != null)
                    _item = _child.GetComponent<Item>();

                if (_child != null && slotButton.GetComponent<Item>().item == _item.item
                && _inventory.slotScripts[a].CountOfItems + 1 <= _item.MaxStackCountInSlot) 
                {
                    _inventory.AddItem(a, gameObject);
                    return;
                }
            }
            
            //Если такого не было найдено, то предмет заберется в ближайщий свободный слот
            
            for (int i = 0; i < _inventory.slots.Length; i++)
            {
                if (_inventory.isFull[i] == false)
                {
					_inventory.AddNewItem(i, slotButton, gameObject);
                    break;
                }
            }
        }
    }
}
