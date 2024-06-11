using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ShootThunder : MonoBehaviour
{
    public TestInputController _inputData;

    private bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        canShoot = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputData._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool buttonValue))
        {
            Debug.Log("A Button: " + buttonValue);
        }
    }

    public void ReadyToShoot()
    {
        canShoot = true;
    }
}
