using UnityEngine;
public class PickUp : LinkManager
{
    private InventorySystem inventory;
    public GameObject slotButton; // item in slot

    private GameObject child;

    private Item item;

    private bool isPickedUp = false;

    private void Start() => inventory = ManagerInventory;

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
                    AddItem(i);
                    break;
				}
                else if (item != null && inventory.slotScripts[i].CountOfItems + 1 > item.MaxStackCountInSlot)
                {
                    i++;
                }

                if (inventory.isFull[i] == false)
                {
					AddItemMain(i);
                    break;
                }
            }
        }
    }

    private void AddItemMain(int i)
    {
        inventory.isFull[i] = true;

        GameObject PickUpedItem = Instantiate(slotButton, inventory.slots[i].transform);

        PickUpedItem.GetComponent<Item>().id = i;

        inventory.slotScripts[i].GetChild();

        inventory.slotScripts[i].isSlotHaveItem = true;

        AddItem(i);
    }

    private void AddItem(int i) 
    {
        inventory.ChangeItemsCount(i, 1);

        inventory.slotScripts[i].UpdateItemsCountField();

        Destroy(gameObject);
    }
}
