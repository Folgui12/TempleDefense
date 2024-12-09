using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXLife : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        Invoke("Dead", 2);
    }
    
    public void Dead()
    {
        Destroy(gameObject);
    }
}
