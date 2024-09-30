using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public Camera CameraRef;
    public bool FirstTimeSeing;
    public bool FirstSteps;
    public bool FirstRotation;
    public bool FirstPinch;
    public bool FirstPoke;
    public bool FirstFistClose;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void StartTutorial()
    {

    }

    public void VisionTutoDone()
    {
        FirstTimeSeing = true; 
    }

}
