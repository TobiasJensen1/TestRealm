using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Combat : MonoBehaviour
{

    public GameObject player;
    float distanceToEnemy;
    Vector3 moveTo;

    bool combatCheck;
    bool idleAttack;
    bool att1;

    float enemyMaxhealth;
    float enemyHealth;
    float currentHealth;
    float playerDamage;
    float damageGiven;

    string attackString;
    float time;
    float damageMultiplier;

    //Used for cooldown
    public List<Skill> skills;


    // Start is called before the first frame update
    void Start()
    {
        skills = GameObject.Find("Canvas").transform.Find("AbilityBar").GetComponent<SkillCoolDown>().skills;
        playerDamage = GameObject.Find("Player").GetComponent<PlayerStats>().damage;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().enemy != null)
        {
            enemyHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().health;
            enemyMaxhealth = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().maxHealth;
            currentHealth = enemyHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth;
        }
        //Bool that is set, given players position relative to enemy
        combatCheck = GameObject.Find("Player").GetComponent<PlayerMovement>().combat;

        combat();
    }



    void combat()
    {

        //Initiate Combat
        if (combatCheck && !idleAttack)
        {
            idleAttack = true;
            if (idleAttack)
            {
                StartCoroutine("IdleAttack");
            }
        }

        //Stop Combat
        if (!combatCheck)
        {
            StopCoroutine("IdleAttack");
            idleAttack = false;
        }

        //Abilities

        //Chose attack to use, checks if skill is on cooldown, idle if none chosen
        if (combatCheck)
        {
            if (Input.GetKey("q"))
            {
                if(skills[0].currentCoolDown >= skills[0].coolDown) { 
                attackString = "Attack1";
                time = 1.6f;
                damageMultiplier = 1.2f;
                skills[0].currentCoolDown = 0;
                }
            }
            if (Input.GetKey("w"))
            {
                if (skills[1].currentCoolDown >= skills[1].coolDown)
                {
                    attackString = "Attack2";
                    time = 2.5f;
                    damageMultiplier = 1.1f;
                    skills[1].currentCoolDown = 0;
                }
                }
            if (Input.GetKey("e"))
            {
                if (skills[2].currentCoolDown >= skills[2].coolDown)
                {
                    attackString = "Taunt";
                    time = 1.6f;
                    damageMultiplier = 0;
                    skills[2].currentCoolDown = 0;
                }
                }
            if (Input.GetKey("r"))
            {
                if (skills[3].currentCoolDown >= skills[3].coolDown)
                {
                    attackString = "Ultimate";
                    time = 4f;
                    damageMultiplier = 4f;
                    skills[3].currentCoolDown = 0;
                }
            }
        }
    }



    IEnumerator IdleAttack()
    {
        while (true)
        {

            if (idleAttack)
            {
                //Plays attack if not null
                if (attackString != null)
                {
                    player.GetComponent<Animator>().Play(attackString);
                    attackString = null;
                    yield return new WaitForSeconds(time - 0.7f);
                    damageEnemy(damageMultiplier);
                    yield return new WaitForSeconds(0.7f);

                }
                //Plays idleattack if no attack chosen
                else
                {
                    skills[4].currentCoolDown = 0;
                    player.GetComponent<Animator>().Play("IdleAttack");
                    //Converts damage to % damage
                    yield return new WaitForSeconds(.9f);
                    damageEnemy(1);
                    yield return new WaitForSeconds(.7f);
                }

            }
        }
    }

    void damageEnemy(float damageMultiplier)
    {
        damageGiven = (enemyMaxhealth * playerDamage) / 100;
        enemyHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth -= playerDamage * damageMultiplier;
        GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().health = (100 / enemyMaxhealth) * GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth;
    }
}


