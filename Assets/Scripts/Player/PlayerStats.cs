using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public float fury;
    public float maxFury;

    public float damage;

    public float level;



    public GameObject healthAndFuryContainer;
    GameObject healthBar;
    GameObject furyBar;



    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        maxFury = fury;

        healthBar = healthAndFuryContainer.transform.Find("PlayerHealthbar").gameObject;
        furyBar = healthAndFuryContainer.transform.Find("PlayerFury").gameObject;


    }

    // Update is called once per frame
    void Update()
    {
        setPlayerHealthAndFury();
    }


    void setPlayerHealthAndFury()
    {

        healthBar.GetComponent<MonoHealthbar>().Health = (int)health;
        furyBar.GetComponent<MonoHealthbar>().Health = (int)fury;

    }
}
