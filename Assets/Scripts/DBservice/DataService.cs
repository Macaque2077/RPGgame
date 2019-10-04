using SQLite4Unity3d;
using UnityEngine;
using System;
using System.Linq;

#if !UNITY_EDITOR
using System.Collections;
using System.IO;
#endif
using System.Collections.Generic;

public class DataService  {

	private SQLiteConnection _connection;

    public SQLiteConnection Connection
    {
        get
        {
            return _connection;
        }
    }

    public DataService(string DatabaseName){

#if UNITY_EDITOR
            var dbPath = string.Format(@"Assets/StreamingAssets/{0}", DatabaseName);
#else
        // check if file exists in Application.persistentDataPath
        var filepath = string.Format("{0}/{1}", Application.persistentDataPath, DatabaseName);

        if (!File.Exists(filepath))
        {
            Debug.Log("Database not in Persistent path");
            // if it doesn't ->
            // open StreamingAssets directory and load the db ->

#if UNITY_ANDROID 
            var loadDb = new WWW("jar:file://" + Application.dataPath + "!/assets/" + DatabaseName);  // this is the path to your StreamingAssets in android
            while (!loadDb.isDone) { }  // CAREFUL here, for safety reasons you shouldn't let this while loop unattended, place a timer and error check
            // then save to Application.persistentDataPath
            File.WriteAllBytes(filepath, loadDb.bytes);
#elif UNITY_IOS
                 var loadDb = Application.dataPath + "/Raw/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);
#elif UNITY_WP8
                var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
                // then save to Application.persistentDataPath
                File.Copy(loadDb, filepath);

#elif UNITY_WINRT
		var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
		
#elif UNITY_STANDALONE_OSX
		var loadDb = Application.dataPath + "/Resources/Data/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
		// then save to Application.persistentDataPath
		File.Copy(loadDb, filepath);
#else
	var loadDb = Application.dataPath + "/StreamingAssets/" + DatabaseName;  // this is the path to your StreamingAssets in iOS
	// then save to Application.persistentDataPath
	File.Copy(loadDb, filepath);

#endif

            Debug.Log("Database written");
        }

        var dbPath = filepath;
#endif
            _connection = new SQLiteConnection(dbPath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
        Debug.Log("Final PATH: " + dbPath);     

	}
    //Start of new functions---------------------------------------------------------------------------
    public void CreateDB(System.Type[] pTableTypes)
    {

        // Gnarly (using Linq) lambda trick in place of a loop, build the "Where"s then execute with a ToList, 
        var createList = pTableTypes.Where<System.Type>(x =>
        {
            _connection.CreateTable(x);
            return true;
        }
            ).ToList();


    }
    public void Store<T>(T Record)
    {
        _connection.InsertOrReplace(Record);

    }

    public void StoreIfNotExists<T>(T Record)
    {

        try
        {
            _connection.Insert(Record);
        }
        catch (Exception E)
        {
            Debug.Log(E.InnerException.Message);
        }

    }

    public void StoreAllIfNotExists<T>(T[] RecordList)
    {

        try
        {
            _connection.InsertAll(RecordList);
        }
        catch (Exception E)
        {
            Debug.Log(E.InnerException.Message);
        }
    }

    public int PlayerCount()
    {
        int result = _connection.Table<Person>().ToList<Person>().Count;
        return result;

    }


    //end of new functions---------------------------------------------------------------------------
    public void CreateDB(){
		_connection.DropTable<Person> ();
		_connection.CreateTable<Person> ();

        _connection.InsertAll(new[]{
            //look into a way topass the character stats to the player to pass the players health---------------------------------------------------
            new Person{
                id = 1,
                name = "player",
                health = 100,
                inventoryList = Inventory.instance.saveInventoryList(),
                equippedList = EquipmentManager.instance.saveEquipmentList()
             }


        }) ;

		
	}



    public IEnumerable<Person> GetPersons(){
		return _connection.Table<Person>();
	}

    public IEnumerable<Person> GetPlayer()
    {
        Debug.Log("4.88888888");
        return _connection.Table<Person>().Where(x => x.id == 1);
    }

    /*	public IEnumerable<Person> GetPersonsNamedRoberto(){
            return _connection.Table<Person>().Where(x => x.Name == "Roberto");
        }

        public Person GetJohnny(){
            return _connection.Table<Person>().Where(x => x.Name == "Johnny").FirstOrDefault();
        }*/
    public Person OverwriteSave()
    {
        Debug.Log("6.1");
        var p = new Person();
        p.id = 1;
        p.name = "player";
        p.health = 100;
        p.inventoryList = Inventory.instance.saveInventoryList();
        p.equippedList = EquipmentManager.instance.saveEquipmentList();
        Debug.Log("6");
        _connection.InsertOrReplace(p);


        return p;
    }

    public Person CreatePerson(){
        
        var p = new Person();
        p.id = 1;
        p.name = "player";
        p.health = 100;
        p.inventoryList = Inventory.instance.saveInventoryList();
        p.equippedList = EquipmentManager.instance.saveEquipmentList();
        Debug.Log("4.0");
        _connection.Insert(p);
        Debug.Log("4.1");



        return p;
	}
}
