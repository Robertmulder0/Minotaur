using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private float speed = 50f;
    private float destroyTime = 3f;

    public Vector3 target {get; set;}
    public bool hit {get; set;}

    private void OnEnable() 
    {
        Destroy(gameObject, destroyTime);
    }
   

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < speed) //destroy if goes too far
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }
}
