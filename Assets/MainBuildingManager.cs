using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainBuildingManager : MonoBehaviour
{
    [SerializeField] private float life;
    [SerializeField] private GameObject LoseMessage;
    [SerializeField] private GameObject LifeBarCanvas;
    
    private LifeBarManager lifeBar;


    void Start()
    {
        lifeBar = GetComponentInChildren<LifeBarManager>();
        lifeBar.SetHealth(life);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            life -= 10;

            lifeBar.UpdateLifeBar(life);

            StillAlive();
        }
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
