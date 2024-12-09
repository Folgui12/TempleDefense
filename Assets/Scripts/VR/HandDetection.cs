using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandDetection : MonoBehaviour
{
    [SerializeField] private float upperForce;
    private bool alreadyShaking = false;

    public void OnGroundHitted(HoverEnterEventArgs args)
    {
        if(args.interactorObject is XRBaseControllerInteractor controllerInteractor && controllerInteractor != null)
        {
            var controller = controllerInteractor.xrController;

            if(controller.gameObject.CompareTag("RightController") && TestInputController.Instance.GetRightVelocity() > 2f)
            {
                ShakeTheEarth();
            }
            if(controller.gameObject.CompareTag("LeftController") && TestInputController.Instance.GetLeftVelocity() > 2f)
            {
                ShakeTheEarth();
            }
        }
    }

    private void ShakeTheEarth()
    {
        foreach(GameObject enemyObject in ActiveEnemiesManager.Instance.activeEnemies)
        {
            BaseEnemyModel enemyModel = enemyObject.GetComponent<BaseEnemyModel>();

            if(enemyModel != null && !enemyModel.OnHand) 
            { 
                Rigidbody enemyRb = enemyObject.GetComponent<Rigidbody>();

                enemyRb.AddForce(new Vector3(0f, upperForce, 0f), ForceMode.Impulse);
                enemyModel.Stuned = true;
            }
        }
    }
}
