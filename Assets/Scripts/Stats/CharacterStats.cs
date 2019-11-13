using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Linq;
using System.Collections.Generic;

[Serializable]
//part of a charecter defines their stats
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;

    //private DataService Db = new DataService("existing.db");

    //any class can get this value but it can only be set from within this class
    public int actualHealth { get; set; }

    //characters damage and armor stats
    public Stat damage;
    public Stat armor;

    //public Enemy Enemy = new Enemy();

    //sets health to equal max health at the start of the game
    void Awake()
    {
        actualHealth = maxHealth;

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.J))
        {
            TakeDamage(50);
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("armor is " + armor.GetValue());
        //subtracts armor from damage, 
        //as we want armor to be a percentage modifier, we get armor as a percent times this by damage and round to the nearest int. then sub this from the original dmg amount
        damage -= (int)Mathf.Round( damage * ((float)armor.GetValue())/100);



        // subtracts damage from health
        actualHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (actualHealth <= 0)
        {
            Die();

            //Enemy.showCombatUI.SetActive(false);
            //showCombatUI.SetActive(false);
        }
    }

    //handles the character dying. is virtual so we can override for character or player death
    public virtual void Die()
    {

        //die
        Debug.Log(transform.name + "died.");

    }

    public void SavePlayer()
    {
        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };
        jsDrop.Store<Person, JsnReceiver>(new List<Person>
        {
            GameModel.currentPlayer
            //new Person { id = 1, name = "player", health = 100, inventoryList = "2", equippedList = "6 5 4 3", password = "player", score = 100 }

         }, jsnReceiverDel) ;

        //old save
/*        Debug.Log("2");
        ExistingDBScript save = new ExistingDBScript();
        Debug.Log("3");
        save.DBDOME();
        Debug.Log("4");*/
    }

    private void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
    }

    public void LoadPlayer()
    {


        /*        var DB = new DataService("existing.db");
                Person player = DB.CheckLoginExists(GameModel.currentPlayer.name, GameModel.currentPlayer.password);*/
        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };
        jsDrop.Select<Person, JsnReceiver>($"id = {GameModel.currentPlayer.id}", jsnListReceiverDel, jsnReceiverDel);

/*        Debug.Log("inventory list load----------------------------------- " + GameModel.currentPlayer.inventoryList);

        //currentHealth = player.health;
        //load equipped inventory, must be done first as removed equipment will be added to inventory to then be removed by loadItems
        EquipmentManager.instance.LoadEquipment();

        //send items to inventory to be loaded
        try
        {
            Inventory.instance.loadItems(player);
            //tempData.instance.loadItems();
        }
        catch(Exception ex)
        {
            Debug.Log("Enumeration error here: " + ex);
        }

        GameManager.instance.updateScore();*/
        //GameModel.currentPlayer = player;
    }

    private void jsnListReceiverDel(List<Person> pReceived)
    {

        string inventoryList = pReceived[0].inventoryList;
        if (pReceived.Count == 1)
        {
            Debug.Log("1");
            GameModel.currentPlayer = pReceived[0];
            Debug.Log("2");
            EquipmentManager.instance.LoadEquipment();
            Debug.Log("3");
            //send items to inventory to be loaded
            try
            {

                Inventory.instance.loadItems(inventoryList);
                Debug.Log("4");
            }
            catch (Exception ex)
            {
                Debug.Log("Enumeration error here: " + ex);
            }
            GameManager.instance.updateScore();
        }
        else
        {
            Debug.Log("Multiple Saves found, please contact developer or create new save");
        }

        
    }
}
