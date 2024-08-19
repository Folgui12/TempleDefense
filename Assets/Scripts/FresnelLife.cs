using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FresnelLife : MonoBehaviour
{
    public Material material;
    public float opacity = 4;
    public float opacityDecrease = 1;
    public float lifeTime = 0.5f;
    private bool timeToDie = false;
    public float growth = 1;

    void Start()
    {
        material.SetFloat("_Opacity", opacity);
    }
    // Update is called once per frame
    void Update()
    {
        transform.localScale += new Vector3(growth * Time.deltaTime, growth * Time.deltaTime, growth * Time.deltaTime);

        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0.0f)
        {
            timeToDie = true;
        }

        if (timeToDie == true)
        {
            Destroy(gameObject);
        }
        
        opacity -= opacityDecrease * Time.deltaTime;
        opacity = Mathf.Max(opacity, 0);
        material.SetFloat("_Opacity", opacity);
        Debug.Log(opacity);
    }
}
