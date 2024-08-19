using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DisplayInputData : MonoBehaviour
{
    private TestInputController controller;

    void Start()
    {
        controller = GetComponent<TestInputController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(controller._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool button))
        {
            Debug.Log("A button: " + button.ToString());
        }
    }
}
