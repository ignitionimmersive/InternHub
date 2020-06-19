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
    [SerializeField] GameObject theLens;

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
        CheckSelection();
        CheckUIenabled();

        if (isBuildActive)
            ActivateMechanicMode();

        if (isPlaceModeActive)
            placeMode.ActivatePlacement();
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
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                GameObject open = hit.collider.gameObject;
                
                if (open.CompareTag("MechanicPanel"))
                {
                    isBuildActive = true;
                    blueprint.gameObject.SetActive(true);
                    exitMechanic.SetActive(true);

                    foreach (GameObject value in this.theLens.GetComponent<TheParent>().children)
                    {
                        value.gameObject.GetComponent<TheChild>().initialPosition = value.gameObject.transform.position;
                        value.gameObject.GetComponent<TheChild>().initialRotation = value.gameObject.transform.rotation;
                    }

                }

                else if (open.CompareTag("UsagePanel"))
                {
                    // Usage mode.
                    isUseModeActive = true;
                    workBench.GetComponent<Animator>().enabled = true;
                    debug.text = "Usage Ready.";
                    exitUse.SetActive(true);
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

                    // This thing refuses to be turned on!!!
                    exitPlace.SetActive(true);

                    // Spitfire and small-scaled scope are active, deactive the large-scale scope.
                    theLens.SetActive(false);
                    spitfire.SetActive(true);
                    smallLens.SetActive(true);
                }
            }
        }
    }

    private void ActivateMechanicMode()
    {
        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
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
}
      