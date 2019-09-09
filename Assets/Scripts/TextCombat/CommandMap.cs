
using System.Collections.Generic;


public class CommandMap 
{
    private static HashSet<string> commands;

    //holds a hash of the available commands
    public void CommandSet()
    {
        commands = new HashSet<string>();
        commands.Add("attack");
        commands.Add("block");
    }

    public bool validAction(string prCommand)
    {
        CommandSet();
        bool lcResult = false;
        if (commands.Contains(prCommand))
        {
            CombatChoice.instance.GoCombatChoice(prCommand);
            lcResult = true;
        }
        return lcResult;

    }


}
