using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{

    public bool touchInput;

    // Update is called once per frame
    void Update()
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
                    if (Input.GetMouseButtonUp(0))
                    {
                        hit.collider.gameObject.GetComponentInParent<ParentBody>().CheckState();
                        if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.ASSEMBLED)
                        {
                            hit.collider.gameObject.GetComponentInParent<ParentBody>().DismantleAllParts();
                        }
                        else if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.DISMANTLED)
                        {
                            hit.collider.gameObject.GetComponentInParent<ParentBody>().AssembleIndividualParts(hit.collider.gameObject.GetComponent<ChildBody>());
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
                        if (touch.phase == TouchPhase.Ended)
                        {
                            hit.collider.gameObject.GetComponentInParent<ParentBody>().CheckState();
                            if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.ASSEMBLED)
                            {
                                hit.collider.gameObject.GetComponentInParent<ParentBody>().DismantleAllParts();
                            }
                            else if (hit.collider.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE == ParentBody.STATES.DISMANTLED)
                            {
                                hit.collider.gameObject.GetComponentInParent<ParentBody>().AssembleIndividualParts(hit.collider.gameObject.GetComponent<ChildBody>());
                            }
                        }
                    }
                }
            }
        }


    }
}
