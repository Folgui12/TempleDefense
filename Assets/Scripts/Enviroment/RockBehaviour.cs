using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    [SerializeField] private float explosionRadius;
    [SerializeField] private float explosionForce;
    [SerializeField] private int explosionDamage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor") || collision.gameObject.CompareTag("GenericEnemy"))
        {
            //Instantiate(Fresnel, transform.position, transform.rotation); --- AGREGAR ANIMACIÓN DE IMPACTO

            var surroundedEnemies = Physics.OverlapSphere(transform.position, explosionRadius);

            foreach (var enemy in surroundedEnemies)
            {
                var getRB = enemy.GetComponent<Rigidbody>();
                var getEnemyModel = enemy.GetComponent<BaseEnemyModel>();
                var getTrees = enemy.GetComponent<Tree>();

                if (getRB == null) continue;
                    getRB.AddExplosionForce(explosionForce, transform.position - new Vector3(0, 1.5f, 0), explosionRadius);

                if (getEnemyModel != null)
                    getEnemyModel.TakeDamage(explosionDamage);

                if(getTrees != null)
                    Destroy(getTrees.gameObject);
            }

            gameObject.SetActive(false);
        }
    }
}
