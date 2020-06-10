using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{

    public TheParent theParent;
    private bool theParentHasBeenSpawned = false;

    public TheBlueprint theBlueprint;
    private bool theBluePrintHasBeenSpawned = false;

    private GameObject theParent01;
    private GameObject theBluePrint01;

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        { 
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                
                    if (theParentHasBeenSpawned == false)
                    {
                        if (hit.collider.gameObject.name == "Plane")
                        {
                            if (touch.phase == TouchPhase.Ended)
                            {
                                this.theParentHasBeenSpawned = true;
                                theParent01 = (GameObject)GameObject.Instantiate(this.theParent.gameObject, hit.point, Quaternion.identity);
                            }
                        }
                    }

                    else if ((theBluePrintHasBeenSpawned == false) && (theParentHasBeenSpawned = true))
                    {
                        if (hit.collider.gameObject.name == "Plane")
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

        }
    }
}
