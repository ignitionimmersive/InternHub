using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(MainBody))]
public class MechanicsController : MonoBehaviour
{
    public MainBody theBody;

    void Start()
    {
        theBody = this.GetComponent<MainBody>();
    }

    void Update()
    {

        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        Vector3 direction = worldMousePosition - Camera.main.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {
            if (hit.collider.gameObject.tag == "SubPart")
            {
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red);

                switch (theBody.CURRENTSTATE)
                {
                    case MainBody.STATES.ASSEMBLED:
                        {
                            if (Input.GetKey(KeyCode.Mouse1))
                            {
                                this.theBody.DismantleAllParts();
                            }
                            break;
                        }

                    case MainBody.STATES.DISMANTLED:
                        {
                            Debug.Log(hit.transform.name);
                            
                            if (Input.GetKey(KeyCode.Mouse0))
                            {
                                foreach (SubPart values in theBody.subParts)
                                {
                                    if (values.subPartElement == hit.collider.gameObject)
                                    {
                                        //if already dismantled skip
                                        if (values.CURRENTSTATE == SubPart.STATES.DISMANTLED)
                                        {
                                            theBody.ReAssembleIndividualSubPart(values);

                                            if (theBody.totalPartsDismantled <= 0)
                                            {
                                                theBody.CURRENTSTATE = MainBody.STATES.CHANGING;
                                                StartCoroutine(TimeDelay());
                                            }
                                        }
                                    }
                                }
                            }


                            else
                            {
                                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.white);
                            }

                            break;
                        }
                }
            }

        }
        else
        {
            Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.blue);

        }
    }

    IEnumerator TimeDelay()
    {
        yield return new WaitForSeconds(1);
        theBody.CURRENTSTATE = MainBody.STATES.ASSEMBLED;
    }
}


