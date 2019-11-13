using UnityEngine;
using System;

//default script for items

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item"; //items already have a name so new is used to override this name
    [SerializeField]
    public Sprite icon = null;
    //for loading the right texture
    public UInt32 itemID;
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
        Inventory.instance.itemsChanged();
    }

    //A way to convert save game item back to regular item when loading game


}

[Serializable]
public class SaveGameItem
{
    public string name;

    [SerializeField]
    Sprite m_InSprite;

    public bool isDefaultItem;

    SerializeTexture exportObj = new SerializeTexture();

    UInt32 itemID;

    public SaveGameItem(Item a_item)
    {
        //get items sprite
        m_InSprite = a_item.icon;
        //converting sprite to json readable format - MAKE ANOTHER METHOD TO DO ALL THIS---------------------------------------------------------------------------------------
        Texture2D tex = m_InSprite.texture;
        exportObj.x = tex.width;
        exportObj.y = tex.height;
        exportObj.bytes = ImageConversion.EncodeToPNG(tex);
        

        name = a_item.name;
        //icon = a_item.icon;
        isDefaultItem = a_item.isDefaultItem;

        
    }

    public class SerializeTexture
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] bytes;
    }
}









