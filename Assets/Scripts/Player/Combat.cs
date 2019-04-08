using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Combat : MonoBehaviour
{

    public GameObject player;
    public GameObject enemy;
    float distanceToEnemy;

    bool att;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        initiateCombat();
        combat();
    }

    void initiateCombat()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButton(0) && hit.collider.tag == "Enemy")
            {
                enemy = hit.collider.gameObject;
                distanceToEnemy = Vector3.Distance(player.transform.position, enemy.transform.position);
                if (distanceToEnemy >= 2)
                {
                    player.GetComponent<PlayerMovement>().speed = 5;
                    player.GetComponent<Animation>().Play("Glory_01_Run_01");
                    player.transform.position = Vector3.MoveTowards(player.transform.position, new Vector3(hit.point.x, 0, hit.point.z), player.GetComponent<PlayerMovement>().speed * Time.deltaTime);
                }
            }
        }
    }


    void combat()
    {
        if(enemy != null)
        {
            if (distanceToEnemy <= 2 && !att)
            {
                player.GetComponent<PlayerMovement>().speed = 0;
                StartCoroutine("atk01");
                att = true;
            }
            if(Input.GetMouseButtonUp(0))
            {
                enemy = null;
            }
        }
    }

    IEnumerator atk01()
    {
        player.GetComponent<Animation>().Play("Glory_01_Atk_01");
        yield return new WaitForSeconds(.5f);
        att = false;
        player.GetComponent<Animation>().Play("Glory_01_Idle_01");
        StopCoroutine("atk01");
    }
}


