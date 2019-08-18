using UnityEngine;

//for the equipment worn by the character
public class EquipmentManager : MonoBehaviour
{
    #region Singleton
    public static EquipmentManager instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    //array of all the items we have equiped
    Equipment[] currentEquipment;

    //delegate to control what happens when inventory changes
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment OldItem);
    public OnEquipmentChanged onEquipmentChanged;

    //inventory variable
    Inventory inventory;

    void Start()
    {
        inventory = Inventory.instance;

        //initialize the array with the number of slots in the enum
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        //use the number of slots from above to initialize the array
        currentEquipment = new Equipment[numSlots];
    }

    //to equip new items
    public void Equip (Equipment newItem)
    {
        //enums are associated with an index
        int slotIndex = (int)newItem.equipSlot;

        //for adding the equipment back to inventory
        Equipment oldItem = null;

        //add the equipment back to our inventory when we swap it out
        if (currentEquipment[slotIndex] != null)
        {
            //set old item to the equipped equipment
            oldItem = currentEquipment[slotIndex];
            //add the old equipment back into inventory
            inventory.Add(oldItem);
        }

        //invoke the delegate qith the relevant variables
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;

    }

    public void Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            //invoke the delegate with the relevant variables, no new item here
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

}
