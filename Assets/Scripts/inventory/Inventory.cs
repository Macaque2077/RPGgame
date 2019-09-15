using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    // using a delegate which subcribes items to be called when the delgate is, useful for the UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    
    public int space = 20;//variable for the size of inventory

    [SerializeField]
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

    //hold the values for loading the equipment
    [SerializeField]
    Equipment[] allEquipment;


    public void loadItems(PlayerSaveData data)
    {

        SaveGameItem.SerializeTexture importObj = new SaveGameItem.SerializeTexture();
        //items = data.wrappedList;
        foreach (SaveGameEquipment item in data.wrappedList)
        {

            Equipment n_Item = ScriptableObject.CreateInstance<Equipment>();
            n_Item = allEquipment[item.itemID];

            Add(n_Item);
        }
    }

}

