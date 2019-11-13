
using System.Collections;
using UnityEngine;

//require the character stats component
[RequireComponent(typeof(CharacterStats))]

//handle the combat
public class CharacterCombat : MonoBehaviour
{
    //if a player is not in combat for this long exit combat
    const float combatCooldown = 5;
    //stores the time of the last attack
    float lastAttackTime;
    //show if player is in combat
    public bool InCombat { get; private set; }

    //delagate with return type of void
    public event System.Action OnAttack;

    //attack timings
    public float attackSpeed = 1f;
    private float attackCooldown = 0f;
    public float attackDelay = .6f;

    CharacterStats myStats;
    // Start is called before the first frame update
    void Start()
    {
        myStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        attackCooldown -= Time.deltaTime;
        if (Time.time - lastAttackTime > combatCooldown)
        {
            InCombat = false;
        }
    }

    public void Attack(CharacterStats targetStats)
    {
        if (attackCooldown <= 0f)
        {
            StartCoroutine(DoDamage(targetStats, attackDelay));

            if (OnAttack != null)
            {
                OnAttack();
            }

            attackCooldown = 1f / attackSpeed;
        }
        InCombat = true;
        lastAttackTime = Time.time;

    }

    IEnumerator DoDamage (CharacterStats stats, float delay)
    {
        yield return new WaitForSeconds(delay);

        stats.TakeDamage(myStats.damage.GetValue());
    }
}
