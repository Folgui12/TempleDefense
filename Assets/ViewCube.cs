using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCube : MonoBehaviour
{
    public LayerMask mask;
    public float fullTime;

    private RaycastHit hit;

    private float timeView;
    private float r;

    private void Start()
    {
        r = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
        {
            if (hit.collider.gameObject.layer == 18)
            {
                timeView += Time.deltaTime;
                r -= Time.deltaTime;  
                hit.collider.GetComponent<MeshRenderer>().material.color = new Color(r/10, 1, 1);
                if(timeView > fullTime)
                {
                    TutorialManager.Instance.VisionTutoDone();
                    hit.collider.gameObject.SetActive(false);
                    this.enabled = false;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.forward * 100.0f);
    }
}
