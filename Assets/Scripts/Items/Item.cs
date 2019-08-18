using UnityEngine;
//default script for items

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; //items already have a name so new is used to override this name
    public Sprite icon = null;
    public bool isDefaultItem = false;

    //by marking as virtual we can derive different objects from the Item and decide what we wish to happen to each kind of item
    public virtual void Use()
    {
        //use item
        Debug.Log("item used" + name);
    }

    public void removeFromInventory()
    {
        Inventory.instance.Remove(this);
    }

}
