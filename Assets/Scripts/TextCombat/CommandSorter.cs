using System;
using UnityEngine;

public class CommandSorter : MonoBehaviour
{
    //parse the input text
    public static string Parse(string prCommand)
    {
        prCommand = prCommand.ToLower();
        string[] commandParts = prCommand.Split(' ');
        CommandMap validateChoice = new CommandMap();
        if (commandParts.Length >= 2)
        {
            //prompt user to enter correct command
            return (CombatChoice.instance.AllCombatText += "\n" +  "please enter single word commands");
        }

        else
        {
            if (validateChoice.validAction(prCommand))
            {
                return (CombatChoice.instance.AllCombatText);
            }
            //prompt user to enter correct command
            else return CombatChoice.instance.AllCombatText + "please enter a combat choice";
           

        }
    }
}
