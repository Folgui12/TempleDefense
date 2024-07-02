using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootThunder : MonoBehaviour
{
    [SerializeField] private GameObject ThunderToSpawn;
    [SerializeField] private Transform ShootPoint;
    //[SerializeField] private Material HandChargedMaterial;
    [SerializeField] private GameObject HandLaser;
    
    public bool canShoot = false;

    // Update is called once per frame
    void Update()
    { // PROBAR
        if(TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton))
        {
            if(gripButton)            
                HandLaser.SetActive(true);
            
            else
                HandLaser.SetActive(false);

            if (TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton) &&
                triggerButton && canShoot)
            {
                canShoot = false;
                Shoot();
            }
        }
            
    }

    private void Shoot()
    {
        Instantiate(ThunderToSpawn, ShootPoint.position, 
            Quaternion.Euler(ShootPoint.transform.rotation.x - 90f, ShootPoint.transform.rotation.y, ShootPoint.transform.rotation.z));
    }

    public void CanShootSwitch()
    {
        canShoot = true;
    }
}
