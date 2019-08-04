using System;
using UnityEngine;

public class ItemPickup : Interactable
{

    public Item item; // so that we can associate items with this script

    public override void Interact()
    {
        base.Interact(); //this means it goes into the interactable code and runs everything in the interact method

        Pickup();
    }

     void Pickup()
    {
        Debug.Log("Picking up " + item.name);
        //add to inventory
        bool wasPickedUp =  Inventory.instance.Add(item); // can do this as we made inventory a singleton

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
       
    }
}
