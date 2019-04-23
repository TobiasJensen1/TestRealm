using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthbar : MonoBehaviour
{

    Transform bar;

    // Start is called before the first frame update
    void Start()
    {
        bar = transform.Find("Background").transform.Find("Bar");
    }
    //Used to update healthbars
    public void setHealth(float health)
    {
        if (bar != null)
        {
            bar.localScale = new Vector3(1f, health);
        }
    }
}
