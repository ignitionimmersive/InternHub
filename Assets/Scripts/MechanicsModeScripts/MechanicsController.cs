using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechanicsController : MonoBehaviour
{

    public TheParent theParent;

    public TheBlueprint theBluePrint;

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
                if (hit.collider.gameObject.GetComponent<TheChild>() != null)
                {
                    if (hit.collider.gameObject.GetComponent<TheChild>().CURRENTSTATE == TheChild.CHILD_STATES.ON_BLUEPRINT)
                    {
                        theParent.AssembleAllChildren();
                    }
                    else if (theParent.CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                    {
                        theParent.DismantleAllChildren();
                    }
                }
 
            }

        }
    }


}
