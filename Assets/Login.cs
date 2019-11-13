using UnityEngine.UI;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
using System;

public class Login : MonoBehaviour
{

    #region Singleton
    public static Login instance;

    void Awake()
    {
        instance = this;
    }
    #endregion
    InputField inputName;

    InputField inputPassword;

    Text outputMessage;

    public GameObject textUsername;

    public GameObject textPassword;

    public GameObject textMessage;

    string username, password;

    public void checkLogin()
    {
        Debug.Log("checking login");


        //get the components to check username and password
        inputName = textUsername.GetComponent<InputField>();
        inputPassword = textPassword.GetComponent<InputField>();
        outputMessage = textMessage.GetComponent<Text>();
        username = inputName.text;
        password = inputPassword.text;

        //start OLD data service 
/*        var DB = new DataService("existing.db");
        Person currentplayer = DB.CheckLoginExists(username, password);*/

        //start JSON service and check for logins
        JSONDropService jsDrop = new JSONDropService { Token = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb" };
        
        jsDrop.Select<Person, JsnReceiver>($"name = '{username}'" , ListReceiver,LoadReceiver );


        // OLD load the game scene
/*        if (currentplayer != null)
        {
            
            GameModel.currentPlayer = currentplayer;
            GameModel.currentPlayer.score = 0;
            SceneManager.LoadScene("Scene1");
           
        }
        else
        {
            Debug.Log("No save found");
            outputMessage.text = "No Login found with that username and password please try again";

        }*/

        //pass the current player to be loaded 
        //Person playerLogin = DataService.GetLogin(username);
        }

    public void ListReceiver(List<Person> pReceivedList)
    {
        Debug.Log("Received items " + pReceivedList.Count());
        foreach (Person lcReceived in pReceivedList)
        {
            Debug.Log("Received {" + lcReceived.name + "," + lcReceived.password + "," + lcReceived.score.ToString() + "}");
            if (lcReceived.password == password)
            {
                
                GameModel.currentPlayer = lcReceived;
                GameModel.gameLoaded = true;
                SceneManager.LoadScene("Scene1");
            }
            else
            {
                Debug.Log("Incorrect Password");
                outputMessage.text = "No Login found with that username and password please try again";
            }
        }// for

        // To do: produce an appropriate response
    }

    private void LoadReceiver(JsnReceiver pStrRecObj)
    {
        Debug.Log("No save found");
        outputMessage.text = "No Login found with that username and password please try again";
    }
}
