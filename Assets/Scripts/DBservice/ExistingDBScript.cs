using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class ExistingDBScript : MonoBehaviour {

	public Text DebugText;

	// Use this for initialization
    public void DBDOME()
    {
        Debug.Log("5.1");
        var ds = new DataService("existing.db");
        Debug.Log("5.2");

        //to overwrite the current players save 
        //var p = ds.OverwriteSave();
        //ToConsole(p.ToString());
        ds.OverwriteSave();
        //create a new player save ---------
        //ds.CreatePerson();
        //ToConsole("New person has been created");

        //var p = ds.GetJohnny ();
        //ToConsole(p.ToString());

    }

    //to create a new save 
    public void newSave()
    {
        var ds = new DataService("existing.db");
        //create a new player save ---------
        ds.CreatePerson();
        ToConsole("New person has been created");
    }

    private void ToConsole(IEnumerable<Person> people){
		foreach (var person in people) {
			ToConsole(person.ToString());
		}
	}

	private void ToConsole(string msg){
        Debug.Log(System.Environment.NewLine + msg);
		Debug.Log (msg);
	}

}
