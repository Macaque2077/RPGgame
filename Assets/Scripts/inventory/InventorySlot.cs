using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon;

    // for adding the remove button
    public Button removeButton;

    Item item;

    //handles adding a new item to the inventory slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
        //make the remove button interactable
        removeButton.interactable = true;

    }

    //clears the inventory slot
    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //make remove button not interactable
        removeButton.interactable = false;

    }

    //for when the remove button is pressed
    public void OnRemoveButton()
    {
        Inventory.instance.Remove(item);
    }

    //To use items
    public void UseItem()
    {
        if (item != null){
            item.Use();
        }

    }
}
