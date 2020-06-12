using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;

[RequireComponent(typeof(ARRaycastManager))]
public class SpawningObject : MonoBehaviour
{
    public GameObject Indicator;
    public GameObject placedPrefab;
    //public Text debug;

    private bool objectPlaced = false;
    private bool activeIndicator = false;
    public Pose indicatorPose;

    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }
    
    void Update()
    {
        if (objectPlaced)
            return;

        UpdateIndicatorPose();
        ActiveSpawnIndicator();
        InstantiateModel();
    }

    private void UpdateIndicatorPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        activeIndicator = hits.Count > 0;
        if (activeIndicator)
        {
            indicatorPose = hits[0].pose;

            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
            indicatorPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void ActiveSpawnIndicator()
    {
        if (activeIndicator)
        {
            Indicator.SetActive(true);
            Indicator.transform.SetPositionAndRotation(indicatorPose.position, indicatorPose.rotation);
            objectPlaced = true;
        }         
        else
            Indicator.SetActive(false);
    }

    private void InstantiateModel()
    {
        if (activeIndicator && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Instantiate(placedPrefab, indicatorPose.position, indicatorPose.rotation);
            Indicator.SetActive(false);
            objectPlaced = true;
        }
    }
}
