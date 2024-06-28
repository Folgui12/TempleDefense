using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimMovement : MonoBehaviour
{
    private float speed = 1.0f;
    private Rigidbody rb2d;
    public Transform parent;
    public Vector3 position;
    public Quaternion rotation;
    public GameObject lightning;
    public GameObject explosion;

    private Vector3 posLighntning;

    void Start()
    {
        rb2d = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        rb2d.velocity = new Vector3 (moveHorizontal*-1*speed, 0, moveVertical*-1*speed);
        position = transform.position;
        posLighntning = transform.position;
        posLighntning.y = transform.position.y +20;

        if (Input.GetMouseButtonDown(0))
        {
            InstantiateObject();
        }
    }

    public void InstantiateObject()
    {
        Instantiate(lightning, posLighntning, Quaternion.Euler(90, 0, 0), parent);
        Instantiate(explosion, position, Quaternion.Euler(90, 0, 0), parent);
    }
}
