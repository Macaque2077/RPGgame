using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatChoice : MonoBehaviour
{
    public static CombatChoice instance;

    //to store the target of the player
    public Enemy target;

    #region Singleton
    private void Awake()
    {
        instance = this;
    }

    #endregion

    public string AllCombatText;

    public void GoCombatChoice(string txtInput)
    {
        // enact the combat choice 
        switch (txtInput)
        {
            case "attack":
                target.attacked();
                break;

            case "block":               
                break;

        }
        AllCombatText += "\n" + txtInput;

        Debug.Log(txtInput + "combat choice");
    }
}
