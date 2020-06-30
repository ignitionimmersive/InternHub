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
    // Debug Text.
    public Text debug;
   
    // Exit buttons.
    [SerializeField] GameObject exitMechanic;
    [SerializeField] GameObject exitPlace;
    [SerializeField] GameObject exitLearn;
    [SerializeField] GameObject exitUse;

    // Use mode objects.
    [SerializeField] GameObject workBench;
    [SerializeField] GameObject theLens;

    [SerializeField] GameObject theMapButtons;
    [SerializeField] GameObject theMapHandle;

    // RotateWorkbench Buttons
    [SerializeField] GameObject RotateButtons;

    // Other essential components.
    [SerializeField] TheBlueprint blueprint;
    [SerializeField] TheParent parent;
    [SerializeField] InfoPanel Panel;
    [SerializeField] GameObject logBook;
    [SerializeField] ObjectPlacement placeMode;

    public GameObject arCamera;

    public GameObject theScope;

    public Transform mainContent;

    [HideInInspector]
    public bool isBuildActive;

    [HideInInspector]
    public bool isLearnActive;

    [HideInInspector]
    public bool isPlaceModeActive;

    [HideInInspector]
    public bool isUseModeActive;

    void Update()
    {
        if (isBuildActive)
        {
            ActivateMechanicMode();
        }

        if (isUseModeActive)
        {
            ActivateUsageMode();
        }

        if (isPlaceModeActive)
        {
            ActivatePlaceMode();
        }
        
        if (isLearnActive)
        {
            //theScope.SetActive(false);
            logBook.SetActive(true);
            CheckExitLearn();
        }
        
        CheckSelection();
        CheckUIenabled();
    }

    // The panels here are the buttons actually.
    private void CheckUIenabled()
    {
        if (isBuildActive || isLearnActive || isPlaceModeActive || isUseModeActive)
        {
            foreach (Transform panel in Panel.panels)
            {
                panel.gameObject.SetActive(false);
            }
        }

        if (!isBuildActive && !isLearnActive && !isPlaceModeActive && !isUseModeActive)
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
                
                if (open.CompareTag("MechanicPanel"))
                {
                    isBuildActive = true;
                    blueprint.gameObject.SetActive(true);
                    exitMechanic.SetActive(true);
                    RotateButtons.SetActive(false);

                    

                }
                else if (open.CompareTag("UsagePanel"))
                {
                    if (isUseModeActive == false)
                    {
                        workBench.GetComponent<Animator>().SetInteger("MapController", 1);
                    }
                    // Usage mode.
                    isUseModeActive = true;
                    RotateButtons.SetActive(false);
                    
                    //workBench.GetComponent<Animator>().enabled = true;
       
                    exitUse.SetActive(true);

                    theScope.SetActive(false);
                    
                    theMapButtons.SetActive(true);
       
                }
                else if (open.CompareTag("LearnPanel"))
                {
                    // Learn mode.
                    isLearnActive = true;
                    RotateButtons.SetActive(false);

                    // Turning things on and off.
                    exitLearn.SetActive(true);
                    theScope.SetActive(false);
                  
                }
                else if (open.CompareTag("PlacePanel"))
                {
                    // Place mode.
                    isPlaceModeActive = true;
                    exitPlace.SetActive(true);
                    RotateButtons.SetActive(false);

                    foreach (GameObject value in this.theScope.GetComponent<TheParent>().children)
                    {
                        value.gameObject.GetComponent<TheChild>().initialPosition = value.gameObject.transform.position;
                        value.gameObject.GetComponent<TheChild>().initialRotation = value.gameObject.transform.rotation;
                    }

                    // Spitfire and small-scaled scope are active, deactive the large-scale scope.
                    theScope.transform.Translate(-0.3f, 0f, 0f, Space.Self);
                    placeMode.spitfire.gameObject.SetActive(true);
                }
            }
        }
    }

    private void ActivateMechanicMode()
    {
        //this.blueprint.gameObject.SetActive(true);
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                    {
                        foreach (GameObject value in this.theScope.GetComponent<TheParent>().children)
                        {
                            value.gameObject.GetComponent<TheChild>().initialPosition = value.gameObject.transform.position;
                            value.gameObject.GetComponent<TheChild>().initialRotation = value.gameObject.transform.rotation;
                        }
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
                    // Turn off blueprint.
                    blueprint.gameObject.SetActive(false);

                    // If exit while object is being dismantled, re-assemble object before go back to main menu.
                    if (parent.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                        parent.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();

                    // Turn off exit button.
                    exitMechanic.SetActive(false);

                    // Change button dynamic.
                    if (theScope.GetComponentInChildren<InfoPanel>() != null)
                    {
                        Destroy(theScope.GetComponentInChildren<InfoPanel>());
                    }

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
                   
                    theScope.SetActive(true); 

                    theMapButtons.SetActive(false);

                    if (theScope.GetComponentInChildren<InfoPanel>() != null)
                    {
                        Destroy(theScope.GetComponentInChildren<InfoPanel>());
                    }

                    theLens.SetActive(false);
                    exitUse.SetActive(false);

                    isUseModeActive = false;
                }
                
                if (hit.collider.gameObject == theMapHandle)
                {
                    workBench.GetComponent<Animator>().SetInteger("MapController", 1);
                    theMapButtons.SetActive(true);
                }
                        
            }
        }
    }

    private void CheckExitLearn()
    {
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("GoBack"))
                {
                    logBook.GetComponent<AnimationScript>().CURRENTSTATE = AnimationScript.STATES.OPEN;

                    theScope.SetActive(true);

                    if (theScope.GetComponentInChildren<InfoPanel>() != null)
                    {
                        Destroy(theScope.GetComponentInChildren<InfoPanel>());
                    }

                    logBook.SetActive(false);
                    exitLearn.SetActive(false);

                    GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
                    foreach (GameObject building in buildings)
                    {
                        building.SetActive(false);
                    }

                    isLearnActive = false;
                }
            }
        }
    }

    private void ExitPlace()
    {
        theScope.transform.parent = mainContent;
        theScope.transform.localScale = Vector3.one;

        foreach (GameObject value in this.theScope.GetComponent<TheParent>().children)
        {
            value.transform.position = value.GetComponent<TheChild>().initialPosition;
            value.transform.rotation = value.GetComponent<TheChild>().initialRotation;
        }

        if (placeMode.readyToExit)
        {
            theScope.transform.Translate(0.3f, 0f, 0f, Space.Self);
        }

        // Deactive spifire.
        placeMode.spitfire.gameObject.GetComponent<Animator>().enabled = false;
        placeMode.spitfire.gameObject.SetActive(false);

        theScope.SetActive(true);

        // Make UI buttons persist.
        if (theScope.GetComponentInChildren<InfoPanel>() != null)
        {
            theScope.GetComponentInChildren<InfoPanel>().OpenPanel();
            Destroy(theScope.GetComponentInChildren<InfoPanel>());
        }
          
        exitPlace.SetActive(false);
        //debug.text = theScope.activeSelf.ToString() + " " + theScope.transform.position.ToString();

        isPlaceModeActive = false;
    }

    private void ActivatePlaceMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("GoBack"))
                {
                    ExitPlace();
                }
                else
                {
                    if (placeMode.isAttached)
                    {
                        placeMode.PlacementProcessing();
                    }
                    else
                    {
                        placeMode.ActivatePlacement();
                    }
                }
            }
        }
    }
}
      