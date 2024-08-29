using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILoS
{
    bool CheckRange(Transform target, float range);
    bool CheckView(Transform target);
    bool CheckView(Transform target, LayerMask maskObs);
}
