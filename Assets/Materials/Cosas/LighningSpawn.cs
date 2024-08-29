using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LighningSpawn : MonoBehaviour
{
    public float lifeTime = 0.5f;
    private bool timeToDie = false;
    public GameObject target;
    public float speed;
    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0,speed,0);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
        {
            timeToDie = true;
        }

        if (timeToDie == true)
        {
            Destroy(gameObject);
        }
        
    }
}
