using SQLite4Unity3d;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class Person  {

   
    [PrimaryKey, AutoIncrement]
    //public Boolean isAutoInc { get; set; }  
    public int id { get; set; }
    public string name { get; set; }
    public string password { get; set; }
    public int score { get; set; }
    public int health { get; set; }
    public string inventoryList { get; set; }
    public string equippedList { get; set; }

    public override string ToString()
    {
        return string.Format("[Person:  Id={0}, Name={1}, password={2}, Health={3},  inventoryList={4}, equippedList={5}, score={6}]", id, name, password, health, inventoryList, equippedList, score);
    }
}
