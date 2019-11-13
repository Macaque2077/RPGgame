using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;

public static class AkeakeState
{
    static private string _QRScanned = "";
    static public string QRScanned
    {
        get
        {
            return _QRScanned;
        }
        set
        {
            _QRScanned = value;
        }
    }
    static public int Rotation { get; set; }
    public static DataService DB = new DataService("Cafe.db");

   

    static AkeakeState()
    {

        Debug.Log("REBUILT");
        
       
    }
}
