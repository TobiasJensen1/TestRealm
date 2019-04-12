using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCombat : MonoBehaviour
{
    public List<GameObject> enemies;
    GameObject enemyHealthbarObject;
    public Text enemyName;

    public float x, y, z;

    GameObject enemy;


    // Start is called before the first frame update
    void Start()
    {
        enemies = new List<GameObject>();
        enemyHealthbarObject = GameObject.Find("PlayerGui").transform.Find("EnemyHealthbar").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        enemy = GameObject.Find("Player").GetComponent<PlayerMovement>().enemy;

        guiEnemyHealthAndName();
        enemiesInCombat();
    }



    //Used to turn Gui Enemy Healthbar on/off and set gui enemy name if player is in combat with enemy
    void guiEnemyHealthAndName()
    {
        if (GameObject.Find("Player").GetComponent<PlayerMovement>().combat)
        {
            if (enemy != null)
            {
                enemyHealthbarObject.SetActive(true);
                enemyName.text = enemy.GetComponent<EnemyStats>().EnemyName;
                enemyName.enabled = true;
            }
        }
        else
        {
            enemyHealthbarObject.SetActive(false);
            enemyName.enabled = false;
        }
    }


    //Used to turn enemies healthbar on/off and enable enemy combat based on position relative to player
    void enemiesInCombat()
    {
        //Enables enemy healthbar if within aggroRange and adjusts position and rotation
        for(int i = 0; i < enemies.Count; i++)
        {         
           
                enemies[i].transform.Find("Healthbar").position = new Vector3(enemies[i].transform.position.x + x, enemies[i].transform.position.y + y, enemies[i].transform.position.z + z);
                enemies[i].transform.Find("Healthbar").rotation = Quaternion.Euler(Camera.main.transform.position.x - enemies[i].transform.position.x - 20, 90, enemies[i].transform.Find("Healthbar").rotation.z);
                enemies[i].transform.Find("Healthbar").gameObject.SetActive(true);
        }
    }
}
        


