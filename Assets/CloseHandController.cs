using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class CloseHandController : MonoBehaviour
{
	public InputActionProperty pinchAnimationAction;
	public InputActionProperty gripAnimationAction;

	public Animator handAnimator;

	void Start()
	{
		handAnimator = GetComponent<Animator>();
	}

	void Update()
	{
		float triggerValue = pinchAnimationAction.action.ReadValue<float>();
		handAnimator.SetFloat("Trigger", triggerValue);

		float gripValue = gripAnimationAction.action.ReadValue<float>();
		handAnimator.SetFloat("Grip", gripValue);

	}
}

