using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 [CreateAssetMenu(fileName = "New Equipment", menuName ="Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot;

    public int armorModifier;
    public int damage;
    public int energy;

}

public enum EquipmentSlot { Head, Chest, Legs, Weapon, Tech, feet}