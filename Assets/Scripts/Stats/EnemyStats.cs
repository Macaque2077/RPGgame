using UnityEngine;

//controls what happens when the enemy dies
public class EnemyStats : CharacterStats
{
    //holds the combat UI
    public GameObject combatUI;

    //kill the enemy
    public override void Die()
    {
        base.Die();

        //add ragdoll effect or death animation

        //remove Combat UI
        combatUI.SetActive(false);

        //destroys enemy game object
        Destroy(gameObject);

        GameModel.currentPlayer.score += 10;
        GameManager.instance.updateScore();
    }
}
