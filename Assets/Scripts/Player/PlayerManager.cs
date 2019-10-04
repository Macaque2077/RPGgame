using UnityEngine;
using System.Linq;
using System;

//to keep track of player for enemy ai
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    #region Singleton
    private void Awake()
    {
        instance = this;
    }

    #endregion

    //public gameobject to drag the player into 
    public GameObject player;

    private DataService Db = new DataService("ZUp.db");
    public DataService DB
    {
        get
        {
            return Db;
        }
    }

    public void PlayerSaveManager()
    {
        /*
         * Create database tables if they do not exist.
         * - a call to create a db from a list of System.Type - 
         * - the C# "typeof" operator returns a System.Type value.
         * 
         */
        Db.CreateDB(new[] {
            typeof(Person)
       });

        if (Db.PlayerCount() == 0)
        {
            MakePlayer(1, "player", 100);
        }


    }

    private void MakePlayer(int pID, string pName, int pHealth)
    {
        Db.StoreIfNotExists<Person>(new Person
        {
            id = pID,
            name = pName,
            health = pHealth,
            inventoryList = null,
            equippedList = null
        }) ;
    }
}
