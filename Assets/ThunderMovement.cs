using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Tag"))
            Destroy(gameObject);
    }
}
