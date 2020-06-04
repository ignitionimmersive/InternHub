using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MainBody))]
public class MechanicsController : MonoBehaviour
{
    public enum PARTSSTATE { assembled, dismantled };
    public PARTSSTATE STATEMACHINE;
    public MainBody theBody;

    public int numberOfDismantledItems;

    void Start()
    {
        theBody = this.GetComponent<MainBody>();
        theBody.InitialiseInitialPositionTransform(theBody.subParts);
        //initialise the object

        InitialiseAssembly();
        
    }

    void InitialiseAssembly()
    {
        theBody.ReAssembleAllParts(theBody.subParts);
        STATEMACHINE = PARTSSTATE.assembled;
        numberOfDismantledItems = theBody.subParts.Count;
    }

    void Update()
    {
        if (STATEMACHINE == PARTSSTATE.assembled)
        {
            InitialiseAssembly();

            if (Input.GetKey(KeyCode.Mouse0))
            {
                theBody.DismantleAllParts(theBody.subParts);
                STATEMACHINE = PARTSSTATE.dismantled;
            }
        }

        if (STATEMACHINE == PARTSSTATE.dismantled)
        {

            Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
            Vector3 direction = worldMousePosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {

                if (hit.collider.gameObject.tag == "SubPart")
                {
                    Debug.Log(hit.transform.name);
                    Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.red);
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        foreach (SubPart values in theBody.subParts)
                        {
                            if (values.subPartElement == hit.collider.gameObject)
                            {
                                //if already dismantled skip
                                if (values.dismantled == true)
                                {
                                    theBody.ReAssembleIndividualSubPart(values);

                                    numberOfDismantledItems--;
                                }
                            }
                        }
                    }
                }

                else
                {
                    Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.white);
                }
            }
            else
            {
                Debug.DrawLine(Camera.main.transform.position, worldMousePosition, Color.blue);
            }


            if (numberOfDismantledItems <= 0)
            {
                StartCoroutine(ChangeStateToAssembled(2f));
            }
        }
    }

    IEnumerator ChangeStateToAssembled(float _time)
    {
        //wait few seconds after everything has been dismantled
        theBody.ReAssembleAllParts(theBody.subParts);
        yield return new WaitForSeconds(_time);
        STATEMACHINE = PARTSSTATE.assembled;
    }
}



