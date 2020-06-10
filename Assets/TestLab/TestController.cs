using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class TestController : MonoBehaviour
{

    public TheParent theParent;
    private bool theParentHasBeenSpawned = false;

    public TheBlueprint theBlueprint;
    private bool theBluePrintHasBeenSpawned = false;

    private GameObject theParent01;
    private GameObject theBluePrint01;

    public ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();


    // Update is called once per frame
    void Update()
    {
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

                    if (theParentHasBeenSpawned == false)
                    {
                        this.theParentHasBeenSpawned = true;
                        theParent01 = (GameObject)GameObject.Instantiate(this.theParent.gameObject, pose.position, Quaternion.identity);
                        //theParent01.transform.Translate(0, 2, 0);
                    }

                    else if ((theBluePrintHasBeenSpawned == false) && (theParentHasBeenSpawned = true))
                    {
                        theBluePrint01 = (GameObject)GameObject.Instantiate(this.theBlueprint.gameObject, pose.position, Quaternion.identity);
                        theBluePrint01.transform.Translate(0, 0.3f, 0);
                        theBluePrint01.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                        theParent01.GetComponent<TheParent>().theBlueprint = theBluePrint01.GetComponent<TheBlueprint>();
                        this.theBluePrintHasBeenSpawned = true;
                    }

                    else if ((theBluePrintHasBeenSpawned == true) && (theParentHasBeenSpawned) == true)
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
                                    
                                }


                            }
                        }
                    }

                }
            }


        }
    }
}
