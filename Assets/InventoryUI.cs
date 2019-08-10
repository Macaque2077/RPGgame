using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    

    //to be able to find the children of items parent
    public Transform itemsParent;

    //caching method to speed up the call
    Inventory inventory;

    InventorySlot[] slots;

    // Start is called before the first frame update
    void start()
    {
        //reference to the inventory
        inventory = Inventory.instance;

        //subscribing to the item changed event and calling updateUI whenever that happens
        inventory.onItemChangedCallback += updateUI;

        //to find the inventory slots components
        //as inventory slots are static we only call this in start, if inventory slots are changing this will need to be moved--------------------------
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }


    

    // Update is called once per frame
    void Update()
    {
        
    }

    void updateUI()
    {
        Debug.Log("Updating UI");
    }
       
