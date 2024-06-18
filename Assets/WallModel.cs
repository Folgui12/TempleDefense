using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallModel : TowerModel, IDamageable
{

    public void RotateStructure()
    {
        transform.Rotate(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90f);
    }

    public override void TakeDamage(int damage)
    {
        CurrentLife -= damage;

        if(CurrentLife < 0)
            Dead();
    }
}
