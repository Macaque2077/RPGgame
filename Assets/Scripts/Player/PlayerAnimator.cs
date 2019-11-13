using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : characterAnimator
{
    //holds the animations for the weapon
    public WeaponAnimations[] weaponAnimations;
    Dictionary<Equipment, AnimationClip[]> weaponAnimationsDict;

    //subscribe to on equipment changed
    protected override void Start()
    {
        base.Start();
        //when the on equipment changed is called so is this 
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;


        weaponAnimationsDict = new Dictionary<Equipment, AnimationClip[]>();
        //add the aniamtions for the weapons at runtime
        foreach(WeaponAnimations a in weaponAnimations)
        {
            weaponAnimationsDict.Add(a.weapon, a.clips);
        }
    }


    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 1);
            if (weaponAnimationsDict.ContainsKey(newItem))
            {
                currentAttackAnimSet = weaponAnimationsDict[newItem];
            }
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Weapon)
        {
            animator.SetLayerWeight(1, 0);
            currentAttackAnimSet = defaultAttackAnimSet;
        }
        if (newItem != null && newItem.equipSlot == EquipmentSlot.Tech)
        {
            animator.SetLayerWeight(2, 1);
        }
        else if (newItem == null && oldItem != null && oldItem.equipSlot == EquipmentSlot.Tech)
        {
            animator.SetLayerWeight(2, 0);
        }

    }

    //holds the animations for each weapon set
    [System.Serializable]
    public struct WeaponAnimations
    {
        public Equipment weapon;
        public AnimationClip[] clips;
    }
}
