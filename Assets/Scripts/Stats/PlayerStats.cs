﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats  
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;   
    }

    private void OnEquipmentChanged(Equipment newItem, Equipment OldItem)
    {
        //if there is a new item we add its stats to our players armor and damage modifiers
        if (newItem != null)
        {
            armor.AddModifier(newItem.armorModifier);
            damage.AddModifier(newItem.damage);
        }

        //if there was an old item we must remove its stats
        if (OldItem != null)
        {
            armor.RemoveModifier(OldItem.armorModifier);
            damage.RemoveModifier(OldItem.damage);
        }
    }
}