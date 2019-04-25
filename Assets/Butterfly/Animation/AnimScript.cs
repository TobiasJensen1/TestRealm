using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AnimScript : MonoBehaviour
{

    Tween tween;
    // Start is called before the first frame update
    void Start()
    {
         
        StartCoroutine("Anim");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Anim()
    {
        while (true)
        {
            tween = transform.DOMoveX(transform.position.x+0.8f, 1.5F);
            yield return tween.WaitForCompletion();
            tween = transform.DOMoveX(transform.position.x-0.8f, 1.5F);
            yield return tween.WaitForCompletion();
        }
    }
}
