using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagedUpdateBehavior : MonoBehaviour
{
    protected virtual void Start()
    {
        CustomUpdateManager.instance.OnLightUpdate += LightUpdate;
        CustomUpdateManager.instance.OnLightFixedUpdate += LightFixedUpdate;
    }
    // Update is called once per frame
    public void LightUpdate()
    {
        if (enabled)
        {
            CustomLightUpdate();
        }
    }
    protected virtual void CustomLightUpdate() { }

    public void LightFixedUpdate() 
    {
        if (enabled)
        {
            CustomLightFixedUpdate();
        }
    }
    protected virtual void CustomLightFixedUpdate() { }



    protected virtual void OnDestroy()
    {
        CustomUpdateManager.instance.OnLightUpdate -= LightUpdate;
    }
}
