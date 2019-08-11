using UnityEngine;

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

    private void Start()
    {
        //initialize the array with the number of slots in the enum
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;

        //use the number of slots from above to initialize the array
        currentEquipment = new Equipment[numSlots];
    }

}
