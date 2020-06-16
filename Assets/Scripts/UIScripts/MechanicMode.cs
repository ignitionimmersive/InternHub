using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class MechanicMode : MonoBehaviour
{
    public UIBehaviour action;
    public TheParent theParent;
    public Text debug;
    //private bool theParentHasBeenSpawned = false;

    public TheBlueprint theBlueprint;
    private bool theBluePrintHasBeenSpawned = false;

    //private GameObject theParent01;
    private GameObject theBluePrint01;

    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // Update is called once per frame
    void Update()
    {
        if (!action.isBuildActive)
            return;

        if (Input.touchCount > 0)
        { 
            Touch touch = Input.GetTouch(0);
            arRaycastManager.Raycast(touch.position, hits, TrackableType.Planes);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (touch.phase == TouchPhase.Ended)
            {
                if (hits.Count > 0)
                {
                    Pose pose = hits[0].pose;

                    if (!theBluePrintHasBeenSpawned)
                    {
                        theBluePrint01 = (GameObject)GameObject.Instantiate(this.theBlueprint.gameObject, pose.position, Quaternion.identity);
                        theParent.gameObject.GetComponent<TheParent>().theBlueprint = theBluePrint01.GetComponent<TheBlueprint>();
                        this.theBluePrintHasBeenSpawned = true;
                        debug.text = "Blueprint spawned.";
                    }
                    else
                    {
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
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
                                    action.exitButton.SetActive(true);
                                }
                            }
                            debug.text = "Activate Mechanic.";
                        }
                    }
                }
            }
        }

        if (action.GoBackToMain())
        {
            Destroy(theBluePrint01);
            //theParent.gameObject.GetComponent<TheParent>().CURRENT_STATE = TheParent.PARENT_STATE.ALL_CHILD_ON_BODY;
        }
    }
}
