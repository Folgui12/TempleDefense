using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Internal;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class WhatAmIGrabbing : MonoBehaviour, IXRHoverInteractable
{
    public XRDirectInteractor Interactor_RH;
    public XRDirectInteractor Interactor_LH;
    public IXRSelectInteractable Interactor_List;

    public HoverEnterEvent firstHoverEntered => throw new NotImplementedException();

    public HoverExitEvent lastHoverExited => throw new NotImplementedException();

    public HoverEnterEvent hoverEntered => throw new NotImplementedException();

    public HoverExitEvent hoverExited => throw new NotImplementedException();

    public List<IXRHoverInteractor> interactorsHovering => throw new NotImplementedException();

    public bool isHovered => throw new NotImplementedException();

    public InteractionLayerMask interactionLayers => throw new NotImplementedException();

    public List<Collider> colliders => throw new NotImplementedException();

    public event Action<InteractableRegisteredEventArgs> registered;
    public event Action<InteractableUnregisteredEventArgs> unregistered;

    public Transform GetAttachTransform(IXRInteractor interactor)
    {
        throw new NotImplementedException();
    }

    public float GetDistanceSqrToInteractor(IXRInteractor interactor)
    {
        throw new NotImplementedException();
    }

    public bool IsHoverableBy(IXRHoverInteractor interactor)
    {
        throw new NotImplementedException();
    }

    public void OnHoverEntered(HoverEnterEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void OnHoverEntering(HoverEnterEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void OnHoverExited(HoverExitEventArgs args)
    {
        throw new NotImplementedException();

    }

    public void OnHoverExiting(HoverExitEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void OnRegistered(InteractableRegisteredEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void OnUnregistered(InteractableUnregisteredEventArgs args)
    {
        throw new NotImplementedException();
    }

    public void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
    {
        throw new NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Interactor_RH != null )
        {
            foreach(var obj in Interactor_RH.interactablesHovered) 
            {
                Debug.Log(obj);
            }
        }

        if( Interactor_LH != null )
        {
            foreach (var obj in Interactor_LH.interactablesHovered)
            {
                Debug.Log(obj);
            }
        }
    }
}
