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
    }
    

    //Updates enemy healthbars
    public void updateHealthbars()
    {
        if (enemies.Contains(transform.gameObject))
        {
            enemyHealthbars.setHealth(currentHealth / maxHealth);
        }

        if(GameObject.Find("Player").GetComponent<PlayerMovement>().enemy != null)
        {
            if(GameObject.Find("Player").GetComponent<PlayerMovement>().enemy == transform.gameObject) { 
            enemyHealthbar.GetComponent<MonoHealthbar>().Health = (int) health;
            }
        }
    }


    //Used to add and remove enemy to list of active enemies, when distance between player and enemy is within aggroRange
    void lookForPlayer()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
      
        if (distanceToPlayer <= aggroRange && !enemies.Contains(transform.gameObject)){
            enemies.Add(transform.gameObject);

        }
        if(distanceToPlayer > aggroRange && enemies.Contains(transform.gameObject))
        {
            enemies.Remove(transform.gameObject);
            transform.gameObject.transform.Find("Healthbar").gameObject.SetActive(false);
           
        }
        
    }

}
