using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallModel : MonoBehaviour, IDamageable
{
    public DefenseStats _stats;

    public float CurrentLife;
    
    // Start is called before the first frame update
    void Start()
    {
        CurrentLife = _stats.Life;
    }

    public void RotateStructure()
    {
        Quaternion.FromToRotation(transform.rotation.eulerAngles, new Vector3(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90));
    }

    public void Dead()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            BulletMovement arrowHit = other.gameObject.GetComponent<BulletMovement>();

            TakeDamage(arrowHit.Damage);
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            BaseEnemyModel enemyRef = other.gameObject.GetComponent<MeleeDamageRef>().EnemyModel;

            TakeDamage(enemyRef._stats.Damage);
        }
    }

    public void TakeDamage(int damage)
    {
        CurrentLife -= damage;

        if(CurrentLife < 0)
            Dead();
    }
}
