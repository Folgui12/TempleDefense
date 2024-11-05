using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TestInputController : MonoBehaviour
{
    public static TestInputController Instance;

    public InputDevice _rightController;
    public InputDevice _leftController;
    public InputDevice _HMD;
    public Vector3 RightControllerVelocity;
    public Vector3 LeftControllerVelocity;


    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_rightController.isValid || !_leftController.isValid || !_HMD.isValid)
        {
            InitializeInputDevices();
            _rightController.TryGetFeatureValue(CommonUsages.deviceVelocity, out RightControllerVelocity);
            _leftController.TryGetFeatureValue(CommonUsages.deviceVelocity, out LeftControllerVelocity);
            Debug.Log("Right:" + RightControllerVelocity.magnitude);
            Debug.Log("Left: " + LeftControllerVelocity.magnitude);
        }
            
    }

    private void InitializeInputDevices()
    {
        if (!_rightController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, ref _rightController);
        if (!_leftController.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, ref _leftController);
        /*if (!_HMD.isValid)
            InitializeInputDevice(InputDeviceCharacteristics.HeadMounted, ref _HMD);*/
    }

    private void InitializeInputDevice(InputDeviceCharacteristics inputCharacteristics, ref InputDevice inputDevice)
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(inputCharacteristics, devices);

        if(devices.Count > 0)
        {
            inputDevice = devices[0];
        }
    }
}
