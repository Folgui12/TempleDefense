using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ShootThunder : MonoBehaviour
{
    [SerializeField] private GameObject ThunderToSpawn;
    [SerializeField] private Transform ShootPoint;

    public bool canShoot = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool button))
        {
            if(button && canShoot)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        Debug.Log("Dispare");
        Instantiate(ThunderToSpawn, ShootPoint.position, ShootPoint.rotation);
        canShoot = false;
    }

    public void CanShootSwitch()
    {
        canShoot = true;
    }
}
