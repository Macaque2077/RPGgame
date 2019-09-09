using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;


public static class SaveLoadManager
{
    //public GameObject player;

    public static void saveGame (CharacterStats PlayerStats)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

        PlayerSaveData data = new PlayerSaveData(PlayerStats);
        bf.Serialize(file, data);
        file.Close();
        
    }


    public static PlayerSaveData Load()
    {
        string path = Application.persistentDataPath + "/playerInfo.dat";
        if (File.Exists(path))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerSaveData data = (PlayerSaveData)bf.Deserialize(file);
            file.Close();

            return data;
        }
        else
        {
            Debug.LogError("save file not found in " + path);
            return null;
        }
    }
}

[Serializable]
public class PlayerSaveData
{
    //public GameObject PlayerSave = PlayerManager.instance.player;
    public int health;
    public float armor;
    public float damage;
    public float[] position;


    public PlayerSaveData(CharacterStats PlayerStats)
    {
        health = PlayerStats.currentHealth;
        armor = PlayerStats.armor.GetValue();
        damage = PlayerStats.damage.GetValue();

/*        position[0] = PlayerStats.transform.position.x;
        position[1] = PlayerStats.transform.position.y;
        position[2] = PlayerStats.transform.position.z;*/
    }
}

