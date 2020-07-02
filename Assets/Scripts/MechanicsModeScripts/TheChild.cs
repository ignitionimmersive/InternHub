using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography;
using UnityEngine;

public class TheChild : MonoBehaviour
{
    public enum CHILD_STATES
    {
        INITIAL_ASSEMBLY,
        DISMANTLE,
        FALLING,
        MOVE_TO_BLUEPRINT,
        MOVING_TO_BLUEPRINT,
        ON_BLUEPRINT,
        MOVE_TO_INITIAL_ASSEMBLY,
        MOVING_TO_INITIAL_ASSEMBLY
    };

    [HideInInspector]
    public Vector3 initialPosition;

    //[HideInInspector]
    public Quaternion initialRotation;

    public CHILD_STATES CURRENTSTATE;

    MoveToAPoint movePoint;
    TheParent parent;
    Rigidbody rigidB;

    private void Awake()
    {
        //this.initialPosition = this.gameObject.GetComponent<Transform>().position;
        //this.initialRotation = this.gameObject.GetComponent<Transform>().rotation;
        
        //movePoint = this.gameObject.GetComponent<MoveToAPoint>();
        parent = this.gameObject.GetComponentInParent<TheParent>();
       //rigidB = this.gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        switch (CURRENTSTATE)
        {
            case CHILD_STATES.INITIAL_ASSEMBLY:
                {
                    this.GetComponentInParent<TheParent>().CURRENT_STATE = TheParent.PARENT_STATE.ALL_CHILD_ON_BODY;
                    this.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                    if (this.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.GetComponent<Rigidbody>());
                    }
                }
                this.parent.CheckParentState();
                break;
            case CHILD_STATES.DISMANTLE:
                { 
                    this.GetComponentInParent<TheParent>().child_on_body_count--;

                    if (this.gameObject.GetComponent<Rigidbody>() == null)
                    {
                        this.gameObject.AddComponent<Rigidbody>();
                    }

                    this.gameObject.GetComponent<Rigidbody>().mass = 100;
                    this.gameObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(1, 1, 0);
                    this.gameObject.GetComponent<Rigidbody>().angularDrag = 100f;
                    //this.gameObject.GetComponent<Rigidbody>().freezeRotation = true;
                    
                    // this.gameObject.GetComponent<Rigidbody>().

                    this.CURRENTSTATE = CHILD_STATES.FALLING;
                    break;
                }
            case CHILD_STATES.FALLING:
                {
                    StartCoroutine(StopFallingAndMoveToBluePrint());
                    break;
                }
            case CHILD_STATES.MOVE_TO_BLUEPRINT:
                {
                    if (this.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.GetComponent<Rigidbody>());
                    }
                    if (this.gameObject.GetComponent<MoveToAPoint>() == null)
                    {
                        this.gameObject.AddComponent<MoveToAPoint>();
                    }

                    

                    int indexInParentBody = this.gameObject.GetComponentInParent<TheParent>().children.IndexOf(this.gameObject);

                    this.gameObject.GetComponent<MoveToAPoint>().finalPosition = this.gameObject.GetComponentInParent<TheParent>().theBlueprint.blueprintPlaceholders[indexInParentBody].transform.position;
                    this.gameObject.GetComponent<MoveToAPoint>().finalRotation = this.gameObject.GetComponentInParent<TheParent>().theBlueprint.blueprintPlaceholders[indexInParentBody].transform.rotation;
                    //movePoint.finalPosition =  parent.theBlueprint.blueprintPlaceholders[indexInParentBody].transform.position;
                    //movePoint.finalRotation =  parent.theBlueprint.blueprintPlaceholders[indexInParentBody].transform.rotation;

                    this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;
                    //movePoint.CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;

                    this.CURRENTSTATE = CHILD_STATES.MOVING_TO_BLUEPRINT;
                    
                    break;
                }
            
            case CHILD_STATES.MOVING_TO_BLUEPRINT:
                {

                    if (this.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.GetComponent<Rigidbody>());
                    }
                    if (this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE == MoveToAPoint.MOVE_TO_A_POINT_STATE.FINAL_POSITION)
                    {
                        Destroy(this.gameObject.GetComponent<MoveToAPoint>());
                        this.CURRENTSTATE = CHILD_STATES.ON_BLUEPRINT;
                        this.GetComponentInParent<TheParent>().child_on_blueprint_count++;
                    }

                }
                break;

            case CHILD_STATES.ON_BLUEPRINT:
                {

                    if (this.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.GetComponent<Rigidbody>());
                    }
                    this.parent.CheckParentState();
                }
                break;
            case CHILD_STATES.MOVE_TO_INITIAL_ASSEMBLY:
                {
                    if (this.gameObject.GetComponent<MoveToAPoint>() == null)
                    {
                        this.gameObject.AddComponent<MoveToAPoint>();
                    }
                    if (this.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.gameObject.GetComponent<Rigidbody>());
                    }

                    this.CURRENTSTATE = CHILD_STATES.MOVING_TO_INITIAL_ASSEMBLY;
                    this.gameObject.GetComponent<MoveToAPoint>().finalPosition = this.initialPosition;
                    this.gameObject.GetComponent<MoveToAPoint>().finalRotation = this.initialRotation;
                    this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;
                    this.GetComponentInParent<TheParent>().child_on_blueprint_count--;
                }
                break;
            case CHILD_STATES.MOVING_TO_INITIAL_ASSEMBLY:
                //this.gameObject.transform.rotation = Quaternion.identity;
                if (this.gameObject.GetComponent<Rigidbody>() != null)
                {
                    Destroy(this.gameObject.GetComponent<Rigidbody>());
                }
                if (this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE == MoveToAPoint.MOVE_TO_A_POINT_STATE.FINAL_POSITION)
                {
                    //this.GetComponentInParent<TheParent>().CheckParentState();
                    Destroy(this.gameObject.GetComponent<MoveToAPoint>());
                    this.CURRENTSTATE = CHILD_STATES.INITIAL_ASSEMBLY;
                    this.GetComponentInParent<TheParent>().child_on_body_count++;
                    //this.GetComponentInParent<TheParent>().CURRENT_STATE = TheParent.PARENT_STATE.ALL_CHILD_ON_BODY;
                }
                break;
        }

        IEnumerator StopFallingAndMoveToBluePrint()
        {
            yield return new WaitForSeconds(2f);
            this.CURRENTSTATE = CHILD_STATES.MOVE_TO_BLUEPRINT;
        }

    }
}
