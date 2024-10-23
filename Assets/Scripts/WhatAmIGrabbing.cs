using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Utilities;
using UnityEngine.XR.Interaction.Toolkit.Utilities.Internal;
using UnityEngine.XR.Interaction.Toolkit;
using System;

public class WhatAmIGrabbing : MonoBehaviour
{
    public XRDirectInteractor Interactor_RH;
    public XRDirectInteractor Interactor_LH;

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
