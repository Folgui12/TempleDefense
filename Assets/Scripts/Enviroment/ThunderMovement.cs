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
    [SerializeField] private GameObject floorVFX;     
    
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
        if(other.gameObject.CompareTag("Floor") || other.gameObject.CompareTag("GenericEnemy") || other.gameObject.CompareTag("Tree"))
        {
            Instantiate(Fresnel, transform.position, transform.rotation);

            if (other.gameObject.CompareTag("Floor"))
            {
                Instantiate(floorVFX, new Vector3(transform.position.x, 0, transform.position.z), floorVFX.transform.rotation);
            }

            var surroundedEnemiesTrees = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach(var collicion in surroundedEnemiesTrees)
            {
                var getRB = collicion.GetComponent<Rigidbody>();
                var getEnemyModel = collicion.GetComponent<BaseEnemyModel>();
                var getTree = collicion.GetComponent<Tree>();

                if (getTree != null)
                {
                    Destroy(getTree.gameObject);
                    getTree.killCollider();
                }


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
