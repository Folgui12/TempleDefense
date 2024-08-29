using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThunderMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    [SerializeField] private float lifeTime;
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private int explosionDamage;
    [SerializeField] private GameObject Fresnel;
     
    private float lifeCounter = 0;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        lifeCounter += Time.deltaTime;

        if(lifeCounter > lifeTime)
            Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Floor"))
        {
            Debug.Log("HITGOUND");
            Instantiate(Fresnel, transform.position, transform.rotation);

            var surroundedEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach(var enemy in surroundedEnemies)
            {
                var getRB = enemy.GetComponent<Rigidbody>();
                var getEnemyModel = enemy.GetComponent<BaseEnemyModel>();

                if (getRB == null) continue;
                getRB.AddExplosionForce(explosionForce, transform.position - new Vector3(0, 1.5f, 0), explosionRadius);

                if (getEnemyModel != null)
                {
                    getEnemyModel.TakeDamage(explosionDamage);
                }
                
            }

            Destroy(gameObject);
        }
            
    }
}
