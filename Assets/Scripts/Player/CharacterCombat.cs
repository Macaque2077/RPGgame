using UnityEngine;

//require the character stats component
[RequireComponent(typeof(CharacterStats))]

//handle the combat
public class CharacterCombat : MonoBehaviour
{

    CharacterStats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }


    public void Attack(CharacterStats targetStats)
    {
        targetStats.TakeDamage(myStats.damage.GetValue());
    }
}
