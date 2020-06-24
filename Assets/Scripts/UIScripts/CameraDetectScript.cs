using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDetectScript : MonoBehaviour
{
    void Update()
    {
        int layerMask = 1 << 8;
        layerMask = ~layerMask;


        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {

            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (hit.distance <= 0.5)
            {
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<TheChild>().CURRENTSTATE == TheChild.CHILD_STATES.ON_BLUEPRINT)
                    {
                        if (hit.collider.gameObject.name == "MainBody_Geo")
                        {
                            hit.collider.gameObject.transform.Rotate(0, 30 * Time.deltaTime, 0);
                        }




                    }
                }
            }
           

        }

    }
}
