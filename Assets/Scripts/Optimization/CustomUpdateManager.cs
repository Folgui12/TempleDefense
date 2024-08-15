using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomUpdateManager : MonoBehaviour
{
    public static CustomUpdateManager instance;
    public event Action OnLightUpdate;
    public event Action OnLightFixedUpdate;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        OnLightUpdate?.Invoke();
    }

    private void FixedUpdate()
    {
        OnLightFixedUpdate?.Invoke();
    }
}
