using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeEnemyMovement : MonoBehaviour
{
    [SerializeField]
    Transform destination;

    NavMeshAgent nma;
    Vector3 targetVector;
    float distanceToPlayer;

    string animationString;
    Animator anim;
    bool attack;
    bool isAttackActive;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nma = GetComponent<NavMeshAgent>();

        if (nma == null)
        {
            print("no navmeshagent");
        }
    }

    // Update is called once per frame
    void Update()
    {
        SetDestinationAndAttack();
    }


    void SetDestinationAndAttack()
    {
        if (transform.GetComponent<EnemyStats>().health > 0)
        {
            if (destination != null)
            {
                distanceToPlayer = Vector3.Distance(transform.position, targetVector);
                targetVector = destination.transform.position;
                //Only move if player is within aggroRange
                if (GetComponent<EnemyStats>().aggroRange > distanceToPlayer)
                {
                    //Move towards player when too far away
                    if (distanceToPlayer >= 2 && !isAttackActive)
                    {
                        anim.Play("Run");
                        transform.LookAt(destination);
                        nma.SetDestination(targetVector);
                        //Start attacking if close to player
                    }
                    else if (distanceToPlayer <= 2 && !isAttackActive)
                    {
                        isAttackActive = true;
                        if (isAttackActive)
                        {
                            StartCoroutine("meleeAttack");
                        }
                    }
                }
            }
        }
        else
        {
            StopCoroutine("meleeAttack");
        }
    }

    public IEnumerator meleeAttack()
    {
        while (true)
        {
            anim.Play("Attack");
            nma.SetDestination(transform.position);
            transform.LookAt(destination);
            yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
            isAttackActive = false;
            GameObject.Find("Player").GetComponent<PlayerStats>().health -= transform.GetComponent<EnemyStats>().damage;
            StopCoroutine("meleeAttack");
        }


    }
}
