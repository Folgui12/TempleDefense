using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewCube : MonoBehaviour
{
    public LayerMask mask;

    private RaycastHit hit;
    private Quaternion correctedRayForward;

    // Update is called once per frame
    void Update()
    {
        correctedRayForward = Quaternion.AngleAxis(transform.rotation.eulerAngles.y, Vector3.up);

        //if (Physics.Raycast(transform.position, correctedRayForward, out hit, 100f))
        //{
        //    if (hit.collider.gameObject.layer == 18)
        //    {   
        //        hit.collider.gameObject.SetActive(false);
        //        this.enabled = false;
        //    }
        //}
        
        Debug.DrawRay(transform.position, transform.forward * 100.0f, Color.green);
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y, transform.position.z * 50));
    }*/
}
