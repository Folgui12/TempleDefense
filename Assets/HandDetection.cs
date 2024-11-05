using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandDetection : MonoBehaviour
{
    public void OnGroundHitted(HoverEnterEventArgs args)
    {
        if(args.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor != null)
        {
            var controller = controllerInteractor.xrController;

            if(controller.gameObject.CompareTag("RightController") && TestInputController.Instance.GetRightVelocity() > 2f)
            {
                Debug.Log("RIGHT HAND HIT");
            }
            if(controller.gameObject.CompareTag("LeftController") && TestInputController.Instance.GetLeftVelocity() > 2f)
            {
                Debug.Log("LEFT HAND HIT");
            }
        }
    }
}
