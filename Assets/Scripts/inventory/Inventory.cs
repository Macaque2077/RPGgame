using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one iinstance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    // using a delegate which subcribes items to be called when the delgate is, useful for the UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    
    public int space = 20;//variable for the size of inventory

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (!item.isDefaultItem)
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room.");
                return false;
            }

            items.Add(item);

            //to trigger everytime something changes in the inventory, updates the UI
            if (onItemChangedCallback != null)
                onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

}

