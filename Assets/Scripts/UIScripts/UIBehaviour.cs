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

    [SerializeField] GameObject exitMechanic;
    [SerializeField] GameObject exitPlace;
    [SerializeField] GameObject exitLearn;
    [SerializeField] GameObject exitUse;
    [SerializeField] GameObject spitfire;
    [SerializeField] GameObject smallLens;
    [SerializeField] GameObject workBench;
    [SerializeField] GameObject theScope;
    [SerializeField] GameObject theLens;

    [SerializeField] GameObject theMapButtons;
    [SerializeField] GameObject theMapHandle;



    [SerializeField] ObjectPlacement placeMode;
    [SerializeField] TheBlueprint blueprint;
    [SerializeField] TheParent parent;
    [SerializeField] InfoPanel Panel;

    [HideInInspector]
    public bool isBuildActive;

    [HideInInspector]
    public bool isLearningActive;

    [HideInInspector]
    public bool isPlaceModeActive;

    [HideInInspector]
    public bool isUseModeActive;

    void Update()
    {
        if (isBuildActive)
            ActivateMechanicMode();
        if (isUseModeActive)
        {
            ActivateUsageMode();
        }

        if (isPlaceModeActive)
        {
            exitPlace.SetActive(true);
            placeMode.ActivatePlacement();
        }
            
        CheckSelection();
        CheckUIenabled();
    }

    // The panels here are the buttons actually.
    private void CheckUIenabled()
    {
        if (isBuildActive || isLearningActive || isPlaceModeActive || isUseModeActive)
        {
            foreach (Transform panel in Panel.panels)
            {
                panel.gameObject.SetActive(false);
            }
        }

        if (!isBuildActive && !isLearningActive && !isPlaceModeActive && !isUseModeActive)
        {
            foreach (Transform panel in Panel.panels)
            {
                panel.gameObject.SetActive(true);
            }
        }
    }

    // Check if user tap on the screen.
    // If yes AND the camera is pointing at the button -> Active the modes.
    private void CheckSelection()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;
                debug.text = open.name;
                if (open.CompareTag("MechanicPanel"))
                {
                    debug.text = "Activate mechanic mode.";
                    isBuildActive = true;
                    blueprint.gameObject.SetActive(true);
                    exitMechanic.SetActive(true);

                    foreach (GameObject value in this.theScope.GetComponent<TheParent>().children)
                    {
                        value.gameObject.GetComponent<TheChild>().initialPosition = value.gameObject.transform.position;
                        value.gameObject.GetComponent<TheChild>().initialRotation = value.gameObject.transform.rotation;
                    }

                }
                else if (open.CompareTag("UsagePanel"))
                {
                    if (isUseModeActive == false)
                    {
                        workBench.GetComponent<Animator>().SetInteger("MapController", 1);
                    }
                    // Usage mode.
                    isUseModeActive = true;
                    
                    //workBench.GetComponent<Animator>().enabled = true;
                    debug.text = "Usage Ready.";
                    //exitUse.SetActive(true);

                    theScope.SetActive(false);
                    
                    theMapButtons.SetActive(true);


                    this.exitUse.SetActive(true);
                    
                }
                else if (open.CompareTag("LearnPanel"))
                {
                    // Learn mode.
                    isLearningActive = true;
                    exitLearn.SetActive(true);
                }
                else if (open.CompareTag("PlacePanel"))
                {
                    // Place mode.
                    isPlaceModeActive = true;

                    // Spitfire and small-scaled scope are active, deactive the large-scale scope.
                    theScope.SetActive(false);
                    spitfire.SetActive(true);
                    smallLens.SetActive(true);
                }
            }
        }
    }

    private void ActivateMechanicMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                debug.text = hit.collider.gameObject.name;
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                    {
                        //debug.text = "Dismantle";
                        hit.collider.gameObject.GetComponentInParent<TheParent>().DismantleAllChildren();
                    }
                    else if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                    {
                        hit.collider.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
                    }
                }

                if (hit.collider.gameObject.CompareTag("GoBack"))
                {
                    debug.text = "EXIT";
                    // Turn off blueprint.
                    blueprint.gameObject.SetActive(false);

                    // If exit while object is being dismantled, re-assemble object before go back to main menu.
                    if (parent.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                        parent.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();

                    // Turn off exit button.
                    exitMechanic.SetActive(false);

                    // Turn of Build mode.
                    isBuildActive = false;
                }
            }
        }
    }

    private void ActivateUsageMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("GoBack"))
                {
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    isUseModeActive = false;

                    //workBench.GetComponent<Animator>().enabled = false;
                    
                    theScope.SetActive(true); 

                    theMapButtons.SetActive(false);

                    theLens.SetActive(false);
                    exitUse.SetActive(false);

                }
                
                if (hit.collider.gameObject == theMapHandle)
                {
                    workBench.GetComponent<Animator>().SetInteger("MapController", 1);
                    theMapButtons.SetActive(true);
                }
                        
            }
        }
    }
}
      