using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.UI.Image;

public class LoS : MonoBehaviour, ILoS
{
    public LayerMask maskObs;

    public bool CheckRange(Transform target, float range)
    {
        float distance = Vector3.Distance(target.position, Origin);
        return distance <= range;
    }
    public bool CheckView(Transform target)
    {
        return CheckView(target, maskObs);
    }
    public bool CheckView(Transform target, LayerMask maskObs)
    {
        Vector3 dirToTarget = target.position - Origin;
        float distance = dirToTarget.magnitude;
        return !Physics.Raycast(Origin, dirToTarget, distance, maskObs);
    }
    public Vector3 Origin => transform.position;
    public Vector3 Forward => transform.forward;
}
