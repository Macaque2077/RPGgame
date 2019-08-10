using UnityEngine;
using UnityEngine.UI;


public class InventorySlot : MonoBehaviour
{
    public Image icon;

    Item item;

    //handles adding a new item to the inventory slot
    public void addItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    //clears the inventory slot
    public void ClearSlot ()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;

    }
}
