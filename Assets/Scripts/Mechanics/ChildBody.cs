using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBody : MonoBehaviour
{
    public enum STATES
    {
        ASSEMBLED,
        DISMANTLED,
        MOVING_TO_BLUEPRINT,
        ON_BLUEPRINT
    };

    [HideInInspector]
    public Vector3 initialPosition;

    [HideInInspector]
    public Quaternion initialRotation;


    [SerializeField]  private STATES currentstate;

    public STATES CURRENTSTATE
    {
        get
        {
            return this.currentstate;
        }
        set
        {
            ChangeState(value);
        }
    }

    private void Start()
    {
        this.initialPosition = this.GetComponent<Transform>().position;
        this.initialRotation = this.GetComponent<Transform>().rotation;
        ChangeState(STATES.ASSEMBLED);
    }

    private void ChangeState(STATES _state)
    {
        switch (_state)
        {
            case STATES.ASSEMBLED:
                {
                    if (this.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.GetComponent<Rigidbody>());
                    }
                    this.transform.position = this.initialPosition;
                    this.transform.rotation = this.initialRotation;
                    this.currentstate = _state;
                    break;
                }
            case STATES.DISMANTLED:
                {
                    if (this.GetComponent<Rigidbody>() == null)
                    {
                        this.gameObject.AddComponent<Rigidbody>();
                    }
                    this.currentstate = _state;
                    break;
                }
            case STATES.MOVING_TO_BLUEPRINT:
                {
                    if (this.GetComponent<Rigidbody>() != null)
                    {
                        Destroy(this.GetComponent<Rigidbody>());

                        int indexInParentBody = this.gameObject.GetComponentInParent<ParentBody>().childParts.IndexOf(this.gameObject);
                        this.gameObject.AddComponent<MoveToBlueprint>().placeholder = this.gameObject.GetComponentInParent<ParentBody>().theBlueprint.blueprintPlaceholders[indexInParentBody];
                    }
                    this.currentstate = _state;
                    break;
                }

            case STATES.ON_BLUEPRINT:
                {
                    if (this.GetComponent<MoveToBlueprint>() != null)
                    {
                        Destroy(this.GetComponent<MoveToBlueprint>());
                    }
                    
                    this.currentstate = _state;
                    break;
                }
        }

        UpdateParentState();
    }

    private void UpdateParentState()
    {
        int i = 0;
        foreach (GameObject values in this.gameObject.GetComponentInParent<ParentBody>().childParts)
        {
            if (values.GetComponent<ChildBody>().CURRENTSTATE == STATES.ASSEMBLED)
            {
                i++;
            }
        }

        if (i == this.gameObject.GetComponentInParent<ParentBody>().childParts.Count)
        {
            this.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE = ParentBody.STATES.ASSEMBLED;
        }

        else
        {
            this.gameObject.GetComponentInParent<ParentBody>().CURRENTSTATE = ParentBody.STATES.DISMANTLED;
        }
    }
}

