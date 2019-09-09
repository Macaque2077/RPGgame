﻿using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable]
//part of a charecter defines their stats
public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;

    //any class can get this value but it can only be set from within this class
    public int currentHealth { get; private set; }

    //characters damage and armor stats
    public Stat damage;
    public Stat armor;

    //public Enemy Enemy = new Enemy();

    //sets health to equal max health at the start of the game
    void Awake()
    {
        currentHealth = maxHealth;

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.U))
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
        currentHealth -= damage;
        Debug.Log(transform.name + " takes " + damage + " damage.");

        if (currentHealth <= 0)
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
        SaveLoadManager.saveGame(this);
    }

    public void LoadPlayer()
    {
        PlayerSaveData data = SaveLoadManager.Load();
        currentHealth = data.health;

/*        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];

        transform.position = position;*/

    }

}
