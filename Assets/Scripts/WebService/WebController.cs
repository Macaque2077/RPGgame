/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.IO;
using System;

public class WebController : MonoBehaviour
{

    private const string API_KEY = "c3f6c81c-0f28-4922-9a1e-9ef0cb0265bb";

    public string playerName;

    private Person GetPerson()
    {
        Debug.Log("in get person");
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://newsimland.com/~todd/JSON/?tok={0},"cmd":{"ALL":"tblPlayers"}} "));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        Debug.Log(response);
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Person info = JsonUtility.FromJson<Person>(jsonResponse);
        Debug.Log(info);
        return info;
    }

    public void LoadonClick()
    {
        Person lcPlayer = GetPerson();
    }
}*/
