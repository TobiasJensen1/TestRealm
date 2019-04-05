using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float smooth;

    Vector3 moveTo;
    Quaternion targetRotation;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        
        targetRotation = transform.rotation;
        GetComponent<Animation>().Play("Glory_01_Idle_01");
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        movement();
        transform.position = Vector3.MoveTowards(transform.position, moveTo, speed * Time.deltaTime);
        transform.LookAt(moveTo, Vector3.up);

        
    }

    



    void movement()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) || Input.GetMouseButton(0))
            {
                speed = 5;
                //changes animaton to run, changes player target position to hit.point, locks y position
                GetComponent<Animation>().Play("Glory_01_Run_01");
                moveTo = hit.point;
                moveTo = new Vector3(hit.point.x, 0, hit.point.z);
            }
        }
        
        if(Vector3.Distance(transform.position, moveTo) < 0.001f)
        {
            GetComponent<Animation>().Play("Glory_01_Idle_01");
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        

        if (!Input.GetMouseButtonDown(0) && collision.transform.tag == "Wall")
        {
            
            speed = 0;
            GetComponent<Animation>().Play("Glory_01_Idle_01");
            
        }
    }


    private void OnCollisionStay(Collision collision)
    {
     if(Input.GetMouseButtonUp(0) && collision.transform.tag == "Wall")
        {
            speed = 0;
            GetComponent<Animation>().Play("Glory_01_Idle_01");
        }   
    }



}
