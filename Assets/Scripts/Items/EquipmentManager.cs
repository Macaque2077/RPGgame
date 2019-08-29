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


    //default item holder
    public Equipment[] defaultItems;

    //target mesh will be used to refer to the player mesh
    public SkinnedMeshRenderer targetMesh;
    //an array of skinned mesh renderers to display the equipment on character in game
    SkinnedMeshRenderer[] currentMeshes;



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

        //initialize the skinned mesh renderer
        currentMeshes = new SkinnedMeshRenderer[numSlots];

        //equip all the default items
        EquipDefaultItems();
    }

    //to equip new items
    public void Equip (Equipment newItem)
    {
        //enums are associated with an index
        int slotIndex = (int)newItem.equipSlot;


        //for adding the equipment back to inventory - unequip anything in the slot first adding it back to inventory
        Equipment oldItem = Unequip(slotIndex);


        //invoke the delegate qith the relevant variables
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        //insert item into the slot
        currentEquipment[slotIndex] = newItem;

        //instantiate the new mesh into the game world
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh);
        newMesh.transform.parent = targetMesh.transform;

        //to get the new mesh to deform based on target bones
        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;

        //insert the new mesh in our current mesh's array
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            //to destroy the mesh when item is removed
            if (currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);

            }

            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            //invoke the delegate with the relevant variables, no new item here
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }

            //returns the old item
            return (oldItem);
        }

        return null;
    }

    //for each item in default items equip it
    void EquipDefaultItems()
    {
        foreach (Equipment item in defaultItems)
        {
            Equip(item);
        }
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
        //equip default items when everything is unequipped
        EquipDefaultItems();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnequipAll();
    }

}
