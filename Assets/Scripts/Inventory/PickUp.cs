using UnityEngine;

public class PickUp : MonoBehaviour
{
    public LinkManager links;

    private InventorySystem inventory;

    public GameObject slotButton; // item in slot

    private GameObject child;

    private Item item;

    private bool isPickedUp = false;

    private void Start() => inventory = links.inventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isPickedUp == false && other.CompareTag("Player"))
        {
            isPickedUp = true;

            //Прокрутка для нахождения уже существующего предмета такого же типа и не превыщающим макс кол-во в одном слоте

            for (int a = 0; a < inventory.slots.Length; a++)
            {
                inventory.slotScripts[a].GetChild();
                child = inventory.slotScripts[a].Child;

                if (child != null)
                    item = child.GetComponent<Item>();

                if (child != null && slotButton.GetComponent<Item>().item == item.item
                && inventory.slotScripts[a].CountOfItems + 1 <= item.MaxStackCountInSlot) 
                {
                    inventory.AddItem(a, gameObject);
                    return;
                    break;
                }
            }
            
            //Если такого не было найдено, то предмет заберется в ближайщий свободный слот
            
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false)
                {
					inventory.AddNewItem(i, slotButton, gameObject);
                    break;
                }
            }
        }
    }
}
