using UnityEngine;

//handles combat interaction with enemy
[RequireComponent(typeof(CharacterStats))]
public class Enemy : Interactable
{
    PlayerManager playerManager;
    CharacterStats myStats;

    //to show the combat UI when in combat
    public GameObject showCombatUI;

    private void Start()
    {
        playerManager = PlayerManager.instance;
        myStats = GetComponent<CharacterStats>();
    }

    public void attacked()
    {
        //attack the enemy
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            showCombatUI.gameObject.SetActive(true);

            //passes this enemy to be attacked by player
            playerCombat.Attack(myStats);
        }
    }

    public override void Interact()
    {
        base.Interact();

        //attack the enemy
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>();
        if (playerCombat != null)
        {
            //shows the UI for player to enter combat commands
            showCombatUI.gameObject.SetActive(true);
            
            //sets this enemy as the players target
            CombatChoice.instance.target = this;

            //passes this enemy to be attacked by player
            playerCombat.Attack(myStats);
        }
        //else showCombatUI.SetActive(false);

    }

    public void stopCombatUI()
    {
        showCombatUI.gameObject.SetActive(false);
    }

}
