using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{
    public bool touchInput;
    // Update is called once per frame
    void Update()
    {
        if (touchInput == false)
        {
            {
                Touch touch = Input.GetTouch(0);
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
                Vector3 direction = worldTouchPosition - Camera.main.transform.position;
                RaycastHit hit;

                if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
                {
                    Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.red);
                    if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                    {
                        Debug.DrawLine(Camera.main.transform.position, worldTouchPosition, Color.white);

                        if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                        {
                            if (touch.phase == TouchPhase.Ended)
                            {
                                hit.collider.gameObject.GetComponentInParent<TheParent>().DismantleAllChildren();
                            }
                        }

                        else if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                        {
                            if (touch.phase == TouchPhase.Ended)
                            {
                                hit.collider.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
                            }
                        }


                    }
                }
            }
        }
        else
        {
            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 100f));
            Vector3 direction = worldMousePosition - Camera.main.transform.position;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red);
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.white);

                    if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            hit.collider.gameObject.GetComponentInParent<TheParent>().DismantleAllChildren();
                        }
                    }

                    else if (hit.collider.gameObject.GetComponentInParent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                    {
                        if (Input.GetMouseButtonUp(0))
                        {
                            hit.collider.gameObject.GetComponentInParent<TheParent>().AssembleAllChildren();
                        }
                    }


                }
            }
        }
        
    }
}
