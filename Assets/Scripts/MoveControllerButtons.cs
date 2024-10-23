using UnityEngine;
using UnityEngine.InputSystem;

public class MoveControllerButtons : MonoBehaviour
{
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    public Animator controllerAnimator;

    private void Start()
    {
        controllerAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        controllerAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        controllerAnimator.SetFloat("Grip", gripValue);
    }
}
