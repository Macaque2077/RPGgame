using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat 
{
    //to enable the baseValue to be shown in unity UI
    [SerializeField]
    private int baseValue;

    // a list of the modifiers to be used in combat
    private List<int> modifiers = new List<int>();

    //to return the base value of the stat
    public int GetValue ()
    {
        //create a value which will be a sum of the modifiers
        int finalValue = baseValue;

        //calculate the sum of the modifiers
        modifiers.ForEach(x => finalValue += x);

        return finalValue;

    }

    //add modifiers to our damage and armor
    public void AddModifier (int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    //removes modifers from damage and armor
    public void RemoveModifier (int modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }

}
