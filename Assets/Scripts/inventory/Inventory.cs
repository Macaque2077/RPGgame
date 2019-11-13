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

    // using a delegate which subcribes items to be called when the delegate is, useful for the UI
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public int space = 20;//variable for the size of inventory

    [SerializeField]
    public List<Item> items = new List<Item>();


    public bool Add(Item item)
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
            {
                Debug.Log("item changed updating UI" + onItemChangedCallback);
                onItemChangedCallback.Invoke();
            }

        }
        itemsChanged();
        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
            onItemChangedCallback.Invoke();
    }

    //hold the values for loading the equipment
    [SerializeField]
    public Equipment[] allEquipment;


    /*    public void loadItems(PlayerSaveData data)
        {
            ClearInventory();
            SaveGameItem.SerializeTexture importObj = new SaveGameItem.SerializeTexture();
            //items = data.wrappedList;
            foreach (SaveGameEquipment item in data.wrappedList)
            {

                Equipment n_Item = ScriptableObject.CreateInstance<Equipment>();
                n_Item = allEquipment[item.itemID];

                Add(n_Item);
            }
        }*/

    //DB Load ------------------------------------------------------------------------------------------------
    public void loadItems(string prInventoryList)
    {
        ClearInventory();
        Debug.Log("A");
/*        if (items.Count == 0)
        {*/
        SaveGameItem.SerializeTexture importObj = new SaveGameItem.SerializeTexture();
        
        string [] savedInventory = prInventoryList.Split(' ');
        Debug.Log("C");
        Debug.Log("Inventory list--------------------------------- = " + savedInventory);

            foreach (string item in savedInventory)
            {
                if (item != "")
                {
                    Debug.Log("Adding the item with ID: " + item);

                    Equipment n_Item = ScriptableObject.CreateInstance<Equipment>();

                    n_Item = allEquipment[int.Parse(item)];

                    Add(n_Item);
                }
            }
/*        }
        else
        {
            Debug.Log("---------------------------------------------------------------------------------------inventory not empty cannot load");
        }*/

    }

    //Test for finding a new way to save for SQL, returns a populated  inventor list to be saved
    //for saving the inventory in sequal used by saveInventoryList
    public List<UInt32> inventoryList = new List<UInt32>();
    public string saveInventoryList()
    {
        string concatInvList = "";
        foreach (Equipment item in items)
        {
            //get the items in inventory
            //inventoryList.Add(new SaveGameEquipment(item).itemID);

            concatInvList = (item.itemID + " " + concatInvList);
            Debug.Log("inventory--------------");
        }
        Debug.Log("Saved the inventory list: " + concatInvList);
        return concatInvList;
    }

    public void ClearInventory()
    {
        //disgusting code to get past enumeration error
        while (items.Count != 0)
        {
            try
            {
                foreach (Equipment item in items)
                {
                    Remove(item);
                }
            }
            catch
            {
                Debug.Log("enumeration error dodged");
            }

        }
    }

    //updates local save to maintain the current items 
    public void itemsChanged()
    {
        GameModel.currentPlayer.inventoryList = saveInventoryList();
    }

}

