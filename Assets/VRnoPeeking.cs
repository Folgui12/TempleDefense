using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRnoPeeking : MonoBehaviour
{
    [SerializeField] LayerMask collisionLayerMask;
    [SerializeField] float fadeSpeed;
    [SerializeField] float sphereCheckSize;

    private Material cameraMat;
    private bool isCameraFadeOut = false;

    private void Awake()
    {
        cameraMat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.CheckSphere(transform.position, sphereCheckSize, collisionLayerMask, QueryTriggerInteraction.Ignore))
        {
            CameraFade(1f);
            isCameraFadeOut=true;
        }
        else
        {
            if (!isCameraFadeOut)
                return;

            CameraFade(0f);
        }
    }

    public void CameraFade(float targetAlpha)
    {
        var fadeValue = Mathf.MoveTowards(cameraMat.color.a, targetAlpha, Time.deltaTime * fadeSpeed);

        cameraMat.color.a = fadeValue;


    }
}
