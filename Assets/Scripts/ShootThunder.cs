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
    [SerializeField] private Animator anim;

    public AudioSource audioSource;

    public bool canShoot = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    { // PROBAR
        if(TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton))
        {
            if(gripButton)
            {
                if(canShoot)
                {
                    anim.SetBool("HasPower", true);
                    AudioManager.Instance.Play("LightningInHand", audioSource);
                    HandLaser.SetActive(true);
                    if (TestInputController.Instance._rightController.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton) &&
                        triggerButton)
                    {
                        canShoot = false;
                        anim.SetBool("HasPower", false);
                        Shoot();
                    }
                }
                else
                    anim.SetBool("HasPower", false);

            }
               
            else
            {
                anim.SetBool("HasPower", false);
                HandLaser.SetActive(false);
            }
                
            
        }
            
    }

    private void Shoot()
    {
        Instantiate(ThunderToSpawn, ShootPoint.position, ShootPoint.rotation);
        AudioManager.Instance.Play("ShootLightning", audioSource);

        //Quaternion.Euler(ShootPoint.transform.rotation.x - 90f, ShootPoint.transform.rotation.y, ShootPoint.transform.rotation.z));
    }

    public void CanShootSwitch()
    {
        canShoot = true;
    }
}
