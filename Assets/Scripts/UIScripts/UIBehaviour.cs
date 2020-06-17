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
    public TheBlueprint blueprint;
    public TheParent parent;
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
        blueprint.gameObject.SetActive(false);
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
            exitButton.SetActive(true);
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
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;

                if (open.CompareTag("MechanicPanel"))
                {
                    isBuildActive = true;
                    debug.text = "Mechanic Ready.";
                    blueprint.gameObject.SetActive(true);

                    ActivateMechanicMode();
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

    private void ActivateMechanicMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                    {
                        hit.collider.gameObject.GetComponentInParent<TheParent>().DismantleAllChildren();
                    }
                    else if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                    {
                        hit.collider.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
                    }
                }
            }
        }

        if (GoBackToMain())
        {
            blueprint.gameObject.SetActive(false);
            if (parent.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                parent.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
        }
    }
}

