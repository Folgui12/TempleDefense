using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRnoPeeking : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayerMask;
    [SerializeField] float sphereCheckSize;
    [SerializeField] GameObject faceQuad;

    private void Awake()
    {
        faceQuad.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayerMask, QueryTriggerInteraction.Ignore))
        {
            faceQuad.SetActive(false);
        }
        else
        {
            faceQuad.SetActive(true);
        }
    }
}
