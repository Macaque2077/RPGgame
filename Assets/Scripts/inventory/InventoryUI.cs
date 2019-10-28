using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    
    //to be able to find the children of items parent (making public variable so we can just drag and drop the parent in the Unity UI)
    public Transform itemsParent;

    //reference to our entire UI
    public GameObject inventoryUI;

    //caching method to speed up the call
    Inventory inventory;

    //inventory array
    InventorySlot[] slots;

    // Start is called before the first frame update
    void Start()
    {
        //reference to the inventory
        inventory = Inventory.instance;

        //subscribing to the item changed event and calling updateUI whenever that happens
        inventory.onItemChangedCallback += UpdateUI;

        //to find the inventory slots components
        //as inventory slots are static we only call this in start, if inventory slots are changing this will need to be moved--------------------------
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }




    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            //setting the inventory UI to its reverse state
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }

    public void UpdateUI()
    {
        //LOOPING through each slot
        for (int i = 0; i < slots.Length; i++)
        {
            //Checking that i does not exceed inventory slots amount
            if (i < inventory.items.Count)
            {
                //adds the items from inventory to the slots
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                //clear slots that dont have an item
                slots[i].ClearSlot();
            }
        }
    }
}
       
