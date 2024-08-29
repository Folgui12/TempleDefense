using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 
public class AnimateHandOnInput : ManagedUpdateBehavior
{

	public InputActionProperty pinchAnimationAction;
	public InputActionProperty gripAnimationAction;
	public Animator handAnimator;

	override protected void CustomLightUpdate()
	{
		base.CustomLightUpdate();
		float triggerValue = pinchAnimationAction.action.ReadValue<float>();

		handAnimator.SetFloat("Trigger", triggerValue);

		float gripValue = gripAnimationAction.action.ReadValue<float>();

		handAnimator.SetFloat("Grip", gripValue);

	}

}
