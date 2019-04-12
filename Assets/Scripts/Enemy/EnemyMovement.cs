using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform destination;

    NavMeshAgent nma;
    Vector3 targetVector;
    float distanceToPlayer;

    string animationString;

    // Start is called before the first frame update
    void Start()   
    {
        GetComponent<Animator>().Play("Idle");
        nma = GetComponent<NavMeshAgent>();

        if(nma == null)
        {
            print("no navmeshagent");
        } else
        {
            //InvokeRepeating("SetDestination", 0f, .1f);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetDestination();
    }


    void SetDestination()
    {
        if(destination != null)
        {
            distanceToPlayer = Vector3.Distance(transform.position, targetVector);
            targetVector = destination.transform.position;
            //Only move if player is within aggroRange
            if (GetComponent<EnemyStats>().aggroRange > distanceToPlayer) { 
                //Stop moving when too close
                if(distanceToPlayer >= 2) {
                    GetComponent<Animator>().Play("Run");
                    transform.LookAt(destination);
                    nma.SetDestination(targetVector);
                } else
                {
                    GetComponent<Animator>().Play("Idle");
                    nma.SetDestination(transform.position);
                    transform.LookAt(destination);
                }
            }
        }
    }

  
}
