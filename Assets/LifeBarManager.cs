using System;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;   

public class LifeBarManager : MonoBehaviour
{
    [SerializeField] private Slider slider;

    public void SetHealth(float maxHealth)
    {
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }

    public void UpdateLifeBar(float currentHealth)
    {
        slider.value = currentHealth;
    }
}
