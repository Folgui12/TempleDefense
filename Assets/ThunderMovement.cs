using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private float timeToDestroy = 4;
    private float timer;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        timer += Time.deltaTime;

        if(timer > timeToDestroy)
        {
            DestroyThunder();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("Enemy"))
            DestroyThunder();
            
    }

    private void DestroyThunder()
    {
        Destroy(gameObject);
    }
}

