﻿using System.Collections;
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
    public GameObject workbench;
    public Button ResetButton;
    public Text debug;

    private bool objectPlaced = false;
    private bool activeIndicator = false;
    private Pose indicatorPose;

    private Vector3 screenCenter;
    private ARRaycastManager arRaycastManager;
    private static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    public static SpawningObject Instance { get; set; }

    public bool IsPlaced
    {
        get
        {
            return objectPlaced;
        }
        set
        {
            objectPlaced = value;
        }
    }

    private void Awake()
    {
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
        ResetButton.onClick.AddListener(OnResetClick);
    }

    void Update()
    {
        UpdateIndicatorPose();
        ActiveSpawnIndicator();

        if (!objectPlaced && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            InstantiateWorkbench();
        }
    }

    private void UpdateIndicatorPose()
    {
        screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        if (!objectPlaced)
        {
            activeIndicator = hits.Count > 0;
            if (activeIndicator)
            {
                indicatorPose = hits[0].pose;

                var cameraForward = Camera.current.transform.forward;
                var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;
                indicatorPose.rotation = Quaternion.LookRotation(cameraBearing);
            }
        }
    }

    private void ActiveSpawnIndicator()
    {
        if (activeIndicator)
        {
            Indicator.SetActive(true);
            Indicator.transform.SetPositionAndRotation(indicatorPose.position, indicatorPose.rotation);
        }
        else
        {
            Indicator.SetActive(false);
        }
    }

    private void InstantiateWorkbench()
    {
        Instantiate(workbench, indicatorPose.position, indicatorPose.rotation);
        objectPlaced = true;
    }

    private void OnResetClick()
    {
        debug.text = "hit";
        UpdatedUIBehaviour.Instance.CurrentMode = ActiveMode.INITIAL;
        //UpdatedUIBehaviour.Instance.StatesSet(UpdatedUIBehaviour.Instance.CurrentMode);
        debug.text = "RESET" + " " + objectPlaced + " " + IsPlaced;
    }
}