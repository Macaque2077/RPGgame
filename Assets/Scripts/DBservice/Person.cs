using SQLite4Unity3d;
using System.Collections.Generic;
using System;
using UnityEngine;


public class Person  {

   
    [PrimaryKey, AutoIncrement]
    public int id { get; set; }
    public string name { get; set; }
    public int health { get; set; }
    public string inventoryList { get; set; }
    public string equippedList { get; set; }


    /*
     *  public int health = 100;
        public List<SaveGameEquipment> inventoryList = new List<SaveGameEquipment>();
        public List<SaveGameEquipment> equippedList = new List<SaveGameEquipment>();

        public Person()
        {
            //health = PlayerStats.currentHealth;
            foreach (Equipment item in Inventory.instance.items)
            {
                //get the items in inventory
                inventoryList.Add(new SaveGameEquipment(item));
                Debug.Log("inventory--------------");
            }
            foreach (Equipment item in EquipmentManager.instance.currentEquipment)
            {
                //get the eqipped items
                if (item != null)
                {
                    equippedList.Add(new SaveGameEquipment(item));
                    Debug.Log("equipped--------------");
                }

            }

        }*/

    public override string ToString()
    {
        return string.Format("[Person:  Id={0}, Name={1}, Health={2},  inventoryList={3}, equippedList={4}]", id, name, health, inventoryList, equippedList);
    }
}
