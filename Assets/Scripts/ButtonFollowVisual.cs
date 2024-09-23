using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class ButtonFollowVisual : MonoBehaviour
{
    public Transform visualTarget;
    public Vector3 localAxis;
    public float resetSpeed;
    public float followAngleThreshold;

    private Vector3 initialLocalPos;

    private Vector3 offset;
    private Transform pokeAttachTransform;


    private XRBaseInteractable interactable;
    private bool isFollowing = false;

    private bool freeze = false;

    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;    

        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(Reset);
        interactable.selectEntered.AddListener(Freeze); 

    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freeze = false;
            }
        }

    }

    public void Reset(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover)
    {
        if (hover.interactorObject is XRPokeInteractor)
        {
            freeze = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (freeze)
            return;

        if(isFollowing && TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton) && 
            gripButton) 
        {
            Vector3 localTargetPosicion = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);

            Vector3 constrainedLocalTargetPosicion = Vector3.Project(localTargetPosicion, localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosicion);
        }
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
