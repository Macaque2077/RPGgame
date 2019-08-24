using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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