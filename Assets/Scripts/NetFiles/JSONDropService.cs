using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using SQLite4Unity3d;
using System.IO;
// using Parse;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Reflection;
using UnityEngine.Networking;

public delegate void ReceiveRecordDelegate<T>(T pStrRecObj);
public delegate void ReceiveRecordDelegateList<T>(List<T> pStrRecObjList);

[Serializable]
public  class JsnReceiver
{
    public String JsnMsg;
    public String Msg;
}

public class JSONDropService 
{
    private  string _URL;
    private DataService DB = new DataService("Cafe.db");
    const string tok = "?tok=";
    public  string Result { get; set; }
    public  string Token { get; set; }
    public  string URL
    {
        get
        {
            return _URL;
        }
        set
        {
            _URL = value;
        }
    }

    public JSONDropService()
    {
        URL = "https://newsimland.com/~todd/JSON/";
    }

    #region Create Table

    enum JsnPState
    {
        JSONSkip = 0,
        JSONStartCurly = 1,
        JSONEndCurly = 2,
        JSONStartArray = 3,
        JSONEndArray = 4,
        JSONStartName = 5,
        JSONEndName = 6
    }
    private  string ReplacePKName(string pJsnString, string pPKName, bool pIsAUTO = false)
    {
        JsnPState currentState = JsnPState.JSONSkip;
        string currentName = "";
        string currentJsn = "";

        foreach (char currentChar in pJsnString)
        {
            
            switch (currentState)
            {

                case JsnPState.JSONSkip:
                    if(currentChar == '{')
                    {
                        currentState = JsnPState.JSONStartCurly;
                    }
                    currentJsn += currentChar;
                    break;
                case JsnPState.JSONStartCurly:
                    if(currentChar == '"')
                    {
                        currentName += '"';
                        currentState = JsnPState.JSONStartName;
                    }
                    else
                        currentJsn += currentChar;
                    break;
                case JsnPState.JSONStartName:
                    if (currentChar == '"')
                    {
                        if (currentName == "\"" + pPKName)
                        {
                            currentJsn += currentName + " PK" + currentChar;
                        }
                        else
                            currentJsn += currentName + currentChar;
                    
                        currentState = JsnPState.JSONSkip;
                    }// if currentChar
                    else
                     currentName += currentChar;
                    break;

            }
        }
        return currentJsn;
    }

    public void Create<T, S>(T pExample, ReceiveRecordDelegate<S> pReceiveGoesHere)
    {
        try
        {
            string result = "";
            bool isAuto = false;
            string tblName = typeof(T).ToString();
            var tblType = typeof(T);
            TableMapping map = DB.Connection.GetMapping(tblType);
            var PrimaryKeys = map.Columns.Where<TableMapping.Column>(
                x =>
                {
                    if (x.IsAutoInc) isAuto = true; // a bit shonky
                    Debug.Log("Make PK --------------");
                    return x.IsPK;
                }
                ).ToList<TableMapping.Column>();
            string pkName = PrimaryKeys.First<TableMapping.Column>().Name;

            string jsnString = JsonConvert.SerializeObject(pExample);
            jsnString = ReplacePKName(jsnString, pkName, isAuto);
            jsnString = "{\"CREATE\":\"" + tblName + "\",\"EXAMPLE\":" + jsnString + "}";
            Debug.Log(jsnString);
            Post<S>(jsnString, pReceiveGoesHere);


            // Return is via pReceiveGoesHere
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }
    #endregion

    #region Store records
    public void Store<T, S>(List<T> pRecords, ReceiveRecordDelegate<S> pReceiveGoesHere)
    {
        try
        {
            string tblName = typeof(T).ToString();
            string jsnList = JsonConvert.SerializeObject(pRecords);//ListToJson<T>(pRecords);
            string jsnString = "{\"STORE\":\"" + tblName + "\",\"VALUE\":" + jsnList + "}";
            Post<S>(jsnString, pReceiveGoesHere);
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }//Store
    #endregion
    #region retrieve ALL records
    public void All<T,S>(ReceiveRecordDelegateList<T> pReceiveSuccessGoesHere, ReceiveRecordDelegate<S> pReceiveFailGoesHere)
    {
        try
        {
            string tblName = typeof(T).ToString();
            string jsnString = "{\"ALL\":\"" + tblName + "\"}";
            Post<T,S>(jsnString, pReceiveSuccessGoesHere,pReceiveFailGoesHere);
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }//Store
    #endregion

    #region SELECT records WHERE
    public void Select<T, S>(string pStrWhere, ReceiveRecordDelegateList<T> pReceiveSuccessGoesHere, ReceiveRecordDelegate<S> pReceiveFailGoesHere)
    {
        try
        {
            string tblName = typeof(T).ToString();
            string jsnString = "{\"SELECT\":\"" + tblName + "\",\"WHERE\":\""+ pStrWhere + "\"}";
            Post<T, S>(jsnString, pReceiveSuccessGoesHere, pReceiveFailGoesHere);
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }//SELECT WHERE
    #endregion
    #region DELETE records WHERE
    public void Delete<T, S>(string pStrWhere, ReceiveRecordDelegate<S> pReceiveGoesHere)
    {
        try
        {
            string tblName = typeof(T).ToString();
            string jsnString = "{\"DELETE\":\"" + tblName + "\",\"WHERE\":\"" + pStrWhere + "\"}";
            Post<S>(jsnString,  pReceiveGoesHere);
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }//DELETE WHERE
    #endregion
    #region DROP table
    public void Drop<T, S>(ReceiveRecordDelegate<S> pReceiveGoesHere)
    {
        try
        {
            string tblName = typeof(T).ToString();
            string jsnString = "{\"DROP\":\"" + tblName + "\"}";
            Post<S>(jsnString, pReceiveGoesHere);
        }
        catch (Exception ex)
        {

            throw (ex);

        }
    }//DROP table

    #endregion
    #region Http with JSONDrop - JSON to/from <T> and List<T>

    private string ListToJson<T>(List<T> pRecordList)
    {
        string lcResult = "[";
        int lcCount = 0;

        foreach (var record in pRecordList)
        {
            if ((lcCount != pRecordList.Count) && (lcCount != 0))
                lcResult += ",";
            lcResult += JsonConvert.SerializeObject(record);

            lcCount++;
        }
        lcResult += "]";
        return lcResult;
    }
    private string GetArrayFromMsg(string pJsn)
    // this only works for a Jsn that has one array of records that do not 
    // contain fields of type array
    {
        // state = 0 looking for '[',
        // state = 1 retrieving adding text between '[' and ']'
        // state = 2 when the enclosing ']' is found
        int state = 0; 
        string lcResult = "";
        foreach( char lcChar in pJsn)
        {
            switch (state)
            {
                case 0:
                    if(lcChar == '[')
                    {
                        state = 1;
                    }
                    break;
                case 1:
                    if (lcChar == ']')
                    {
                        state = 2;

                    }
                    else
                        lcResult += lcChar;
                    break;
                case 2:
                     
                    break;
            }
            
        }
        return lcResult;

    }
    private List<T> FromJsonArrayToList<T>(string pStrJsonAry)
    {
        string[] sep = { "},{" };
        string[] aryOfJSON = (pStrJsonAry.Substring(1, pStrJsonAry.Length - 2)).Split(sep, StringSplitOptions.RemoveEmptyEntries);
        List<string> lstOfJSONClean = new List<string>(); ;
        foreach (string aString in aryOfJSON)
        {
            string bString = aString;
            if (aString.Substring(0, 1) != "{" && aString.Substring(aString.Length - 1, 1) != "}")
            {
                bString = "{" + aString + "}" ;
            }
            else 
            if (aString.Substring(aString.Length - 1, 1) != "}")
            {
                bString = aString + "}";
            }
            else 
            if( aString.Substring(0, 1) != "{")
            {
                bString = "{"+ aString ;
            }
            lstOfJSONClean.Add(bString);

        }
        aryOfJSON = lstOfJSONClean.ToArray();

        List<T> lcResult = new List<T>();
        foreach (string aJSONStr in aryOfJSON)
        {

            lcResult.Add(JsonConvert.DeserializeObject<T>(aJSONStr));
        }

        return lcResult;
    }

    /*
     * Posts the JSON to JSONDrop 
     * 
     * returns control asynchonously to the delegate
     */

    private UnityWebRequest jsnWebRequest(string pJsn)
    {
        string URIJsn = URL + tok + "{\"tok\":\"" + Token + "\",\"cmd\":" + pJsn + "}";
        System.Uri lcURI = new System.Uri(URIJsn);

        UnityWebRequest lcWebReq = new UnityWebRequest(lcURI, "GET")
        {
            downloadHandler = new DownloadHandlerBuffer(),

        };
        return lcWebReq;
    }
    private void Post<T,S>(string pJsn, ReceiveRecordDelegateList<T> pReceiveSuccessGoesHere, ReceiveRecordDelegate<S> pReceiveFailGoesHere)
    {
        UnityWebRequest lcWebReq = jsnWebRequest(pJsn);

        // VERY WEIRD, because Send returns an object that
        // you then add a "completed" event handler to the
        // completed listeners
        var lcAsyncOp = lcWebReq.SendWebRequest();
        lcAsyncOp.completed += (x => {
            Debug.Log("RETURNED FROM JsnDROP:" + lcWebReq.downloadHandler.text);
            string lcStrJsnReceived = lcWebReq.downloadHandler.text;
            string lcJsnMsgArray = "";

            // THIS IS LOOKING MESSY!! 
            S jsnAsTypeS = JsonUtility.FromJson<S>(lcStrJsnReceived); 
            JsnReceiver jsnReceived = jsnAsTypeS as JsnReceiver;
            List<T> receivedList;
            switch (jsnReceived.JsnMsg)
            {
                case "SUCCESS.ALL":
                case "SUCCESS.SELECT":
                    lcJsnMsgArray = GetArrayFromMsg(lcStrJsnReceived);

                    receivedList = FromJsonArrayToList <T>(lcJsnMsgArray);
                    Debug.Log("Post<T,S> LIST length is " + receivedList.Count().ToString());
                    pReceiveSuccessGoesHere(receivedList);
                    break;
                default:
                    pReceiveFailGoesHere(jsnAsTypeS);
                    break;
            }
            
        });
    }

    private   void Post<S>(string pJsn, ReceiveRecordDelegate<S> pReceiveGoesHere)
    {
        

        UnityWebRequest lcWebReq = jsnWebRequest(pJsn);

        // VERY WEIRD, because Send returns an object that
        // you then add a "completed" event handler to the
        // completed listeners
        var lcAsyncOp = lcWebReq.SendWebRequest();
        lcAsyncOp.completed += (x => {
            Debug.Log("RETURNED FROM JsnDROP:" + lcWebReq.downloadHandler.text);

            pReceiveGoesHere(JsonUtility.FromJson<S>(lcWebReq.downloadHandler.text));
        });


    }
    #endregion




}
