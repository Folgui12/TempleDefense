using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

public class GhostArcherTower : MonoBehaviour
{
    [SerializeField] private DefenseStats archerStats;
    [SerializeField] private LineRenderer circle;

    private void Update()
    {
        DrawCirlce(100, archerStats.AttackRange);
    }

    void DrawCirlce(int steps, float radius)
    {
        circle.positionCount = steps;

        for(int currentStep = 0; currentStep < steps; currentStep++)
        {
            float circumferenceProgress = (float)currentStep / steps;

            float currentRadian = circumferenceProgress * 2 * Mathf.PI;

            float xScaled = Mathf.Cos(currentRadian);
            float yScaled = Mathf.Sin(currentRadian);

            float x = xScaled * radius;
            float y = yScaled * radius;

            Vector3 currentPosition = new Vector3 (transform.position.x + x, 0, transform.position.z + y);

            circle.SetPosition(currentStep, currentPosition);
        }
    }
}

