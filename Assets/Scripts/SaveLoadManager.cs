using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine;
using System.Collections.Generic;


public static class SaveLoadManager
{
    //Old save
    #region
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
    #endregion
    //Json  Save

}

[Serializable]
public class PlayerSaveData
{
    //public GameObject PlayerSave = PlayerManager.instance.player;
    public int health;

    public float[] position;

    public List<SaveGameEquipment> wrappedList = new List<SaveGameEquipment>();

    public Equipment[] currentEquipment;
    


    public PlayerSaveData(CharacterStats PlayerStats)
    {
        health = PlayerStats.currentHealth;
        foreach (Equipment item in Inventory.instance.items)
        {
            wrappedList.Add(new SaveGameEquipment(item));
            
        }
    }

}

public class JSONsave 
{

    [SerializeField]
    Sprite m_InSprite;

    [ContextMenu("serialize")]
    public void SerializeTest(CharacterStats PlayerStats)
    {
        PlayerSaveData data = new PlayerSaveData(PlayerStats);
        string text = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/playerInfo.dat", text);
    }

    [ContextMenu("deserialize")]
    public PlayerSaveData DeSerializeTest()
    {
        string text = File.ReadAllText(Application.persistentDataPath + "/playerInfo.dat");
        //importObj = JsonUtility.FromJson<SerializeTexture>(text);
        PlayerSaveData data = JsonUtility.FromJson<PlayerSaveData>(text);
        return data;
    }
}

/*public class JSONsave : MonoBehaviour
{

    [SerializeField]
    Sprite m_InSprite;

    SerializeTexture exportObj = new SerializeTexture();
    SerializeTexture importObj = new SerializeTexture();

    [ContextMenu("serialize")]
    public void SerializeTest()
    {
        Texture2D tex = m_InSprite.texture;
        exportObj.x = tex.width;
        exportObj.y = tex.height;
        exportObj.bytes = ImageConversion.EncodeToPNG(tex);
        string text = JsonUtility.ToJson(exportObj);
        File.WriteAllText(@"d:\test.json", text);
    }

    [ContextMenu("deserialize")]
    public void DeSerializeTest()
    {
        string text = File.ReadAllText(@"d:\test.json");
        importObj = JsonUtility.FromJson<SerializeTexture>(text);
        Texture2D tex = new Texture2D(importObj.x, importObj.y);
        ImageConversion.LoadImage(tex, importObj.bytes);
        Sprite mySprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), Vector2.one);
        GetComponent<Image>().sprite = mySprite;
    }
    [Serializable]
    public class SerializeTexture
    {
        [SerializeField]
        public int x;
        [SerializeField]
        public int y;
        [SerializeField]
        public byte[] bytes;
    }
}*/


