using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ObjectPlacement : MonoBehaviour
{
    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            arRaycastManager.Raycast(Input.GetTouch(0).position, hits, TrackableType.Planes);

            if (hits.Count > 0)
            {
                Pose pose = hits[0].pose;
                transform.position = pose.position;
                transform.rotation = pose.rotation;
            }
        }
    }
}
