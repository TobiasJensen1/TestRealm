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


    // Start is called before the first frame update
    void Start()
    {
        
        playerDamage =  GameObject.Find("Player").GetComponent<PlayerStats>().damage;

    }

    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("Player").GetComponent<PlayerMovement>().enemy != null) { 
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

        //Chose attack to use, idle if none chosen
        if (combatCheck) { 
            if(Input.GetKey("space"))
            {
                attackString = "Attack1";
            }
        }
    }


    
    IEnumerator IdleAttack()
    {
        while (true) {

            if (idleAttack)
            {
                //Plays attack if not null
                if(attackString != null)
                {
                    player.GetComponent<Animator>().Play(attackString);
                    attackString = null;
                    yield return new WaitForSeconds(1.6f);
                }
                //Plays idleattack if no attack chosen
                else
                {
                    player.GetComponent<Animator>().Play("IdleAttack");
                    //Converts damage to % damage
                   

                    
                    yield return new WaitForSeconds(1f);
                    damageGiven = (enemyMaxhealth * playerDamage) / 100;
                    enemyHealth = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth -= playerDamage;
                    GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().health = (100 / enemyMaxhealth) * GameObject.Find("Player").GetComponent<PlayerMovement>().enemy.GetComponent<EnemyStats>().currentHealth;
                    yield return new WaitForSeconds(.6f);
                }
                
            }
        }
    }
}


