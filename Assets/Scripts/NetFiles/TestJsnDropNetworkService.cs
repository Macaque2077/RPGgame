using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestJsnDropNetworkService : MonoBehaviour
{
    public void jsnReceiverDel(JsnReceiver pReceived)
    {
        Debug.Log(pReceived.JsnMsg + " ..." + pReceived.Msg);
        // To do: parse and produce an appropriate response
    }

    public void jsnListReceiverDel(List<Person> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count());
        foreach (Person lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.name + "," + lcReceived.password + "," + lcReceived.score.ToString()+"}");
        }// for

        // To do: produce an appropriate response
    }

    public void loadPlayerSave()
    {
        string currentPlayerID = ("id = " + GameModel.currentPlayer.id);
        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };
        jsDrop.Select<Person, JsnReceiver>("id = 1", jsnListReceiverDel, jsnReceiverDel);
    }

    public void testGetSave()
    {
        Debug.Log("trying to retrieve ");


        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };

        //drop the table
        //jsDrop.Drop<Person, JsnReceiver>(jsnReceiverDel);

        //CREATE THE TABLE
        /*        jsDrop.Create<Person, JsnReceiver>(new Person
                {
                    //isAutoInc = true,
                    id = 123,
                    name = "playerhsbfibs",
                    health = 100,
                    inventoryList = "6 5 4 32222222222222222222222222222222222222222222222222222222222222222222222",
                    equippedList = "6 5 4 32222222222222222222222222222222222222222222222222222222222222222222222",
                    password = "player22222222222222222",
                    score = 100000
                }, jsnReceiverDel);*/

        //store the data
        jsDrop.Store<Person, JsnReceiver>(new List<Person>
                                        {
                                            new Person{id = 1, name = "player", health = 100, inventoryList = "2 ", equippedList = "6 5 4 3", password = "player", score = 100},
                                            new Person{id = 2, name = "Sam", health = 100, inventoryList = "2 ", equippedList = "6 5 4 3", password = "player", score = 100},
                                            new Person{id = 3, name = "jeff", health = 100, inventoryList = "2 ", equippedList = "6 5 4 3", password = "player", score = 100}

                                         }, jsnReceiverDel);

        //retrieve the players save 
        //jsDrop.All<Person, JsnReceiver>(jsnListReceiverDel, jsnReceiverDel);
        Debug.Log("drop table");


    }

    // Start is called before the first frame update
    void Start()
    {
        #region Test jsn drop
        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };
        /*
        // Create table person
        jsDrop.Create<tblPerson, JsnReceiver>(new tblPerson
        {
            PersonID = "UUUUUUUUUUUUUUUUUUUUUUUUUUU",
            HighScore = 0,
            Password = "***************************"
        }, jsnReceiverDel);

        // Store people records
        jsDrop.Store<tblPerson,JsnReceiver> (new List<tblPerson>
        {
            new tblPerson{PersonID = "Jack",HighScore = 100,Password ="test"},
            new tblPerson{PersonID = "Jonny",HighScore = 200,Password ="test1"},
            new tblPerson{ PersonID = "Jill",HighScore = 300,Password ="test2"}
         }, jsnReceiverDel);
        
        // Retreive all people records
        jsDrop.All<tblPerson, JsnReceiver>(jsnListReceiverDel, jsnReceiverDel);
        
        jsDrop.Select<tblPerson,JsnReceiver>("HighScore > 200",jsnListReceiverDel, jsnReceiverDel);
        
        jsDrop.Delete<tblPerson, JsnReceiver>("PersonID = 'Jonny'", jsnReceiverDel);
        */
        //jsDrop.Drop<Person, JsnReceiver>(jsnReceiverDel);
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
