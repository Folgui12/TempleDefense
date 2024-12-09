using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MainBuildingManager : ManagedUpdateBehavior
{
    [SerializeField] private float life;
    [SerializeField] private GameObject LoseMessage;
    [SerializeField] private GameObject LifeBarCanvas;
    [SerializeField] private Transform Player;
    [SerializeField] private GameObject DirectionalLight;
    private LifeBarManager lifeBar;
    override protected void Start()
    {
        lifeBar = GetComponentInChildren<LifeBarManager>();
        lifeBar.SetHealth(life);
    }

    override protected void CustomLightUpdate()
    {
        base.CustomLightUpdate();
        LifeBarCanvas.transform.LookAt(Player);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("EnemyArrow"))
        {
            if(other.gameObject.CompareTag("Enemy"))
                TakeDamage(other.gameObject.GetComponent<BaseEnemyModel>()._stats.Damage);

            if (other.gameObject.CompareTag("EnemyArrow"))
                TakeDamage(other.gameObject.GetComponent<BulletMovement>().Damage);

            Light light = DirectionalLight.GetComponent<Light>();
            

            Color currentColor = light.color;
            currentColor.r -= 0.0f;      // No modificas el rojo
            currentColor.g -= 25 / 255f; // Resta 25 al verde
            currentColor.b -= 25 / 255f; // Resta 25 al azul

            currentColor.r = Mathf.Clamp01(currentColor.r);
            currentColor.g = Mathf.Clamp01(currentColor.g);
            currentColor.b = Mathf.Clamp01(currentColor.b);

            light.color = currentColor;
            lifeBar.UpdateLifeBar(life);

            StillAlive();
        }
    }

    public void TakeDamage(int damage)
    {
        life -= damage;
    }

    private void StillAlive()
    {
        if(life <= 0)
        {
            LoseMessage.SetActive(true);
            LifeBarCanvas.SetActive(false);
            Debug.Log("PERDISTE");
        }
    }
}
