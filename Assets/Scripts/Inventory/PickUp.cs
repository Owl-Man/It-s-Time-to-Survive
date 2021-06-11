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
        if (other.CompareTag("Player") && isPickedUp == false)
        {
            isPickedUp = true;
            
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                inventory.slotScripts[i].GetChild();
                child = inventory.slotScripts[i].Child;

                if (child != null)
                    item = child.GetComponent<Item>();

				if (child != null && slotButton.GetComponent<Item>().item == item.item
                && inventory.slotScripts[i].CountOfItems + 1 <= item.MaxStackCountInSlot) 
				{
                    inventory.AddItem(i, gameObject);
                    break;
				}
                else if (item != null && inventory.slotScripts[i].CountOfItems + 1 > item.MaxStackCountInSlot)
                {
                    i++;
                }

                if (inventory.isFull[i] == false)
                {
					inventory.AddItemMain(i, slotButton, gameObject);
                    break;
                }
            }
        }
    }
}
