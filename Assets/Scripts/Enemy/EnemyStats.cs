using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public string EnemyName;

    public float maxHealth;
    public float currentHealth;
    public float health;

    public float damage;
    public float aggroRange;

    List<GameObject> enemies;
    GameObject player;
    float distanceToPlayer;

    public EnemyHealthbar enemyHealthbars;

    GameObject enemyHealthbar;
    bool death;

    // Start is called before the first frame update
    void Start()
    {

        maxHealth = health;
        health = 100;
        currentHealth = maxHealth;
        enemyHealthbar = GameObject.Find("PlayerGui").transform.Find("EnemyHealthbar").gameObject;
        enemies = GameObject.Find("CombatController").GetComponent<EnemyCombat>().enemies;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        lookForPlayer();
        updateHealthbars();
        destroyWhenDead();
    }


    //Updates enemy healthbars
    public void updateHealthbars()
    {
        if (enemies.Contains(transform.gameObject))
        {
            enemyHealthbars.setHealth(currentHealth / maxHealth);
        }

        if (GameObject.Find("Player").GetComponent<PlayerMovement>().enemy != null)
        {
            if (GameObject.Find("Player").GetComponent<PlayerMovement>().enemy == transform.gameObject)
            {
                enemyHealthbar.GetComponent<MonoHealthbar>().Health = (int)health;
            }
        }
    }



    //Destroy and remove enemy from list when health <= 0, disables box collider and makes enemy null to stop combat and collision while death animation plays
    void destroyWhenDead()
    {
        if (health <= 0 && !death)
        {
            death = true;
            health = 0;
            currentHealth = 0;
            enemies.Remove(transform.gameObject);
            transform.GetComponent<Animator>().Play("Death");
            transform.GetComponent<BoxCollider>().enabled = false;
            GameObject.Find("Player").GetComponent<PlayerMovement>().enemy = null;
            Destroy(transform.gameObject, 3f);
        }
    }



    //Used to add and remove enemy to list of active enemies, when distance between player and enemy is within aggroRange
    void lookForPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);

        if (distanceToPlayer <= aggroRange && !enemies.Contains(transform.gameObject))
        {
            enemies.Add(transform.gameObject);

        }
        if (distanceToPlayer > aggroRange && enemies.Contains(transform.gameObject))
        {
            enemies.Remove(transform.gameObject);
            transform.gameObject.transform.Find("Healthbar").gameObject.SetActive(false);

        }

    }

}
