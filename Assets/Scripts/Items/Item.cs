using UnityEngine;
//default script for items

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; //items already have a name so new is used to override this name
    public Sprite icon = null;
    public bool isDefaultItem = false;

}
