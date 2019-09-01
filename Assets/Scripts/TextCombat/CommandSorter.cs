using System;
using UnityEngine;
using UnityEngine.UI;

public class CommandSorter : MonoBehaviour
{

    public static string Parse(string prCommand)
    {
        prCommand = prCommand.ToLower();
        string[] commandParts = prCommand.Split(' ');
        CommandMap validateChoice = new CommandMap();
        if (commandParts.Length >= 2)
        {
            return (CombatChoice.instance.AllCombatText += "\n" +  "please enter single word commands");
        }

        else
        {
            if (validateChoice.validAction(prCommand))
            {
                return (CombatChoice.instance.AllCombatText);
            }
            //CombatChoice.instance.GoCombatChoice(prCommand);
            else return CombatChoice.instance.AllCombatText + "please enter a combat choice";
           

        }
    }
}
