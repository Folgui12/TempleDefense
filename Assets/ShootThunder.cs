using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootThunder : MonoBehaviour
{
    [SerializeField] private GameObject ThunderToSpawn;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private Material HandChargedMaterial;
    [SerializeField] private GameObject HandLaser;
    
    public bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { // PROBAR
        if(TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton))
        {
            if(gripButton)            
                HandLaser.SetActive(true);
            
            else
                HandLaser.SetActive(false);

            if (TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool ThumbButton) && 
                ThumbButton && canShoot)
            {
                canShoot = false;
                Shoot();
            }
        }
            
    }

    private void Shoot()
    {
        Instantiate(ThunderToSpawn, ShootPoint.position, ShootPoint.rotation);
    }

    public void CanShootSwitch()
    {
        canShoot = true;
    }
}
