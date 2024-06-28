using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject lightning;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(lightning, transform.position, Quaternion.identity);
        }
    }
}
