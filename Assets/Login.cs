﻿using UnityEngine.UI;
using UnityEngine;

public class Login : MonoBehaviour
{
    InputField inputName;

    InputField inputPassword;

    public GameObject textUsername;

    public GameObject textPassword;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void checkLogin()
    {
        //get the components to check username and password
        inputName = textUsername.GetComponent<InputField>();
        inputPassword = textPassword.GetComponent<InputField>();
        string username = inputName.text;
        string password = inputPassword.text;

        //check check db with the username
        //check if password is right 
        Person currentplayer = DataService.CheckLoginExists(username, password);

        //load the game scene
        
        //pass the current player to be loaded 
        /* Person playerLogin = DataService.GetLogin(username);*/
    }
}
