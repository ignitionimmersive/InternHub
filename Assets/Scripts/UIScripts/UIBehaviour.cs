using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.CompilerServices;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class UIBehaviour : MonoBehaviour
{
    public Text debug;
    public GameObject exitButton;
    public ARPlaneManager planeManager;
    public InfoPanel Panel;
  
    [HideInInspector]
    public bool isBuildActive;

    [HideInInspector]
    public bool isLearningActive;

    [HideInInspector]
    public bool isPlaceModeActive;

    [HideInInspector]
    public bool isUseModeActive;

    private void Start()
    {
        exitButton.SetActive(false);
    }

    void Update()
    {
        CheckSelection();
        CheckUIenabled();
    }

    private void CheckUIenabled()
    {
        if (isBuildActive || isLearningActive || isPlaceModeActive || isUseModeActive)
        {
            foreach (Transform panel in Panel.panels)
            {
                panel.gameObject.SetActive(false);
                debug.text = "In Mechanic Mode";
            }    
            //exitButton.SetActive(true);
        }

        if (!isBuildActive && !isLearningActive && !isPlaceModeActive && !isUseModeActive)
        {
            foreach (Transform panel in Panel.panels)
            {
                panel.gameObject.SetActive(true);
                debug.text = "In Main Menu";
            }
            exitButton.SetActive(false);
        }
    }

    public bool GoBackToMain()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject button = hit.collider.gameObject;

                if (button.CompareTag("GoBack"))
                {
                    isBuildActive = isLearningActive = isPlaceModeActive = isUseModeActive = false;
                    return true;
                }
            }
        }
        return false;
    }

    private void CheckSelection()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;

                if (open.CompareTag("MechanicPanel"))
                {
                    isBuildActive = true;
                    debug.text = "Mechanic Ready.";

                    // Tap on the indicator go back to the main hub.
                    // if (GoBackToMain)
                    // Destroy(body)
                    // Destroy(blueprint)
                    // enableUI
                }

                else if (open.CompareTag("UsagePanel"))
                {
                    // Usage mode.
                    isUseModeActive = true;
                }
                else if (open.CompareTag("LearnPanel"))
                {
                    // Learn mode.
                    isLearningActive = true;
                }
                else if (open.CompareTag("PlacePanel"))
                {
                    // Place mode.
                    isPlaceModeActive = true;
                }
            }
        }
    }
}

