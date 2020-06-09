using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public Text debug;


    //Sam's part of code for testing
    public TheParent theParent;
    private bool theParentHasBeenSpawned = false;

    public TheBlueprint theBlueprint;
    private bool theBluePrintHasBeenSpawned = false;

    private GameObject theParent01;
    private GameObject theBluePrint01;

    ///ends hers
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            debug.text = "Not found";

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
                Vector3 direction = worldTouchPosition - Camera.main.transform.position;
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject open = hit.collider.gameObject;

                    if (open.CompareTag("MechanicButton"))
                    {
                        if (theParentHasBeenSpawned == false)
                        {
                            
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    this.theParentHasBeenSpawned = true;
                                    theParent01 = (GameObject)GameObject.Instantiate(this.theParent.gameObject, hit.point, Quaternion.identity);
                                
                            }
                            
                        }
                        else if ((theBluePrintHasBeenSpawned == false) && (theParentHasBeenSpawned = true))
                        {
                            
                                if (touch.phase == TouchPhase.Ended)
                                {
                                    theBluePrint01 = (GameObject)GameObject.Instantiate(this.theBlueprint.gameObject, hit.point, Quaternion.identity);
                                    theBluePrint01.transform.Translate(0, 7, 0);
                                    theBluePrint01.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                                    theParent01.GetComponent<TheParent>().theBlueprint = theBluePrint01.GetComponent<TheBlueprint>();
                                    this.theBluePrintHasBeenSpawned = true;
                                }
                            

                        }


                        else if ((theBluePrintHasBeenSpawned == true) && (theParentHasBeenSpawned) == true)
                        {
                            Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.red);

                            if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                            {

                                if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                                {
                                    Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.white);

                                    if (touch.phase == TouchPhase.Ended)
                                    {
                                        hit.collider.gameObject.GetComponentInParent<TheParent>().DismantleAllChildren();
                                    }
                                }

                                else if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                                {
                                    Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.white);
                                    if (touch.phase == TouchPhase.Ended)
                                    {
                                        hit.collider.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
                                    }
                                }


                            }
                        }
                    }
                    else if (open.CompareTag("UsageButton"))
                    {
                        // Usage mode.
                    }
                    else if (open.CompareTag("LearnButton"))
                    {
                        // Learn mode.
                    }
                    else if (open.CompareTag("PlaceButton"))
                    {
                        // Place mode.
                    }
                }
            }
        }
    }
}
