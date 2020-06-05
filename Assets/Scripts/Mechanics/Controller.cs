using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool touchInput;

    private bool turnOffInputs = false;

    // Update is called once per frame
    void Update()
    {
        switch (turnOffInputs)
        {
            case false:
                {
                    if (!touchInput)
                    {
                        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
                        Vector3 direction = worldMousePosition - Camera.main.transform.position;
                        RaycastHit hit;
                        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
                        {
                            Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red);
                            if (hit.collider.gameObject.GetComponent<ChildBody>() != null)
                            {
                                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.white);

                                if (hit.collider.gameObject.GetComponent<ChildBody>().CURRENTSTATE == ChildBody.STATES.DISMANTLED)
                                {
                                    if (Input.GetMouseButtonUp(0))
                                    {
                                        hit.collider.gameObject.GetComponentInParent<ParentBody>().MoveAllPartsToBlueprint();
                                       
                                    }
                                }

                                else if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.ASSEMBLED)
                                {
                                    if (Input.GetMouseButtonUp(0))
                                    {
                                        hit.collider.gameObject.GetComponentInParent<ParentBody>().DismantleAllParts();
                                      
                                    }
                                }

                                

                                else if (hit.collider.gameObject.GetComponent<ChildBody>().CURRENTSTATE == ChildBody.STATES.ON_BLUEPRINT)
                                {
                                    if (Input.GetMouseButtonUp(0))
                                    {
                                        hit.collider.gameObject.GetComponentInParent<ParentBody>().AssembleAllParts();
                                        
                                    }
                                }


                            }
                        }
                    }
                    else
                    {
                        if (Input.touchCount > 0)
                        {
                            Touch touch = Input.GetTouch(0);
                            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
                            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
                            RaycastHit hit;
                            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
                            {
                                Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.red);
                                if (hit.collider.gameObject.GetComponent<ChildBody>() != null)
                                {
                                    Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.white);



                                    if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.ASSEMBLED)
                                    {
                                        if (touch.phase == TouchPhase.Ended)
                                        {
                                            hit.collider.gameObject.GetComponentInParent<ParentBody>().DismantleAllParts();
                                        }
                                    }

                                    else if (hit.collider.gameObject.GetComponent<ChildBody>().CURRENTSTATE == ChildBody.STATES.DISMANTLED)
                                    {
                                        if (touch.phase == TouchPhase.Ended)
                                        {
                                            hit.collider.gameObject.GetComponentInParent<ParentBody>().MoveAllPartsToBlueprint();
                                        }
                                    }

                                    else if (hit.collider.gameObject.GetComponent<ChildBody>().CURRENTSTATE == ChildBody.STATES.ON_BLUEPRINT)
                                    {
                                        if (touch.phase == TouchPhase.Ended)
                                        {
                                            hit.collider.gameObject.GetComponentInParent<ParentBody>().AssembleAllParts();
                                        }
                                    }
                                }
                            }
                        }
                    }

                    break;
                }
        }


    }


    IEnumerator TurnOffInputs()
    {
        this.turnOffInputs = true;
        yield return new WaitForSeconds(1);
        this.turnOffInputs = false;
    }
}
