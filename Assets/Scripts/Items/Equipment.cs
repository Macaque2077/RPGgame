using UnityEngine;
using System;


[Serializable]
 [CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    // slots to store equipment
    public EquipmentSlot equipSlot;

    // skinned mesh renderer for displaying armor - essentially adds the mesh to the equipment so we can see the actual blender object as the item
    public SkinnedMeshRenderer mesh;

    //modifiers for armor, damage and energy weapons
    public int armorModifier;
    public int damage;
    public int energy;

    //overrides item use
    public override void Use()
    {
        base.Use();
        // equip the item

        EquipmentManager.instance.Equip(this);
        //remove from inventory
        removeFromInventory();

    }

}

//Creates the different types of equipment 
public enum EquipmentSlot { Head, Chest, Legs, Weapon, Tech, feet}

[Serializable]
public class SaveGameEquipment
{
    public string name;

    [SerializeField]
    Sprite m_InSprite;

    //for saving icon
    public int itemID;


    public bool isDefaultItem;

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh;
    public int armorModifier;
    public int damage;
    public int energy;

    SerializeTexture exportObj = new SerializeTexture();
    

    public SaveGameEquipment(Equipment a_item)
    {
        //get items sprite
        /*        m_InSprite = a_item.icon;
                //converting sprite to json readable format - MAKE ANOTHER METHOD TO DO ALL THIS---------------------------------------------------------------------------------------
                Texture2D tex = m_InSprite.texture;
                exportObj.x = tex.width;
                exportObj.y = tex.height;
                exportObj.bytes = ImageConversion.EncodeToPNG(tex);

                iconID = a_item.iconID;
                name = a_item.name;
                equipSlot = a_item.equipSlot;
                mesh = a_item.mesh;
                armorModifier = a_item.armorModifier;
                damage = a_item.damage;
                energy = a_item.energy;
                isDefaultItem = a_item.isDefaultItem;*/

        itemID = a_item.itemID;

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