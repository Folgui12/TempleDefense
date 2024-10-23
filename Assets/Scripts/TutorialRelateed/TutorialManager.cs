using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;

    public Camera CameraRef;

    [SerializeField] private GameObject textView;
    [SerializeField] private GameObject textMove;
    [SerializeField] private GameObject textRotation;
    [SerializeField] private GameObject textCanvas;
    [SerializeField] private GameObject WorldIlumination;
    [SerializeField] private GameObject SpotLight;
    [SerializeField] private List<GameObject> innerWalls;
    [SerializeField] private VRnoPeeking noPeekingRef;

    public float walkTime;
    private float walkTimer;

    public float rotationRounds;
    public float delayBetweenRotationTime;
    private float rotationCounter;
    private float delayBetweenRotationTimer;

    private bool FirstTimeSeing;
    private bool FirstSteps;
    private bool FirstRotation;
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
        if(FirstTimeSeing && !FirstSteps)
        {
            if(TestInputController.Instance._leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftJoystick) && leftJoystick.magnitude != 0)
            {
                walkTimer += Time.deltaTime;
                if(walkTimer > walkTime)
                {
                    FirstStepsDone();
                }
            }
        }
        if(FirstSteps && !FirstRotation)
        {
            delayBetweenRotationTimer += Time.deltaTime;
            if (TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightJoystick) && rightJoystick.x != 0)
            {
                if(delayBetweenRotationTimer > delayBetweenRotationTime)
                {
                    rotationCounter++;
                    delayBetweenRotationTimer = 0;
                }
                if (rotationCounter >= rotationRounds)
                {
                    FirstRotationsDone();
                    GameManager.Instance.firstTutoFinish = true;
                }
            }
        }
    }

    public void FirstRotationsDone()
    {
        FirstRotation = true;
        textCanvas.SetActive(false);
        WorldIlumination.SetActive(true);
        SpotLight.SetActive(false);

        foreach (GameObject wall in innerWalls)
        {
            wall.SetActive(false);

            if(wall.name == "BasicSpace")
                noPeekingRef.enabled = false;
        }
    }

    public void FirstStepsDone()
    {
        FirstSteps = true;
        textMove.SetActive(false);
        textRotation.SetActive(true);
    }

    public void VisionTutoDone()
    {
        FirstTimeSeing = true;
        textView.SetActive(false);
        textMove.SetActive(true);
    }

}
