using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentBody : MonoBehaviour
{
    public enum STATES 
    { 
        ASSEMBLED, 
        DISMANTLED 
    };

    public List<GameObject> childParts;

    public STATES CURRENTSTATE;

    private void Start()
    {
        InitialiseChildBodyComponents();
    }
    private void InitialiseChildBodyComponents()
    {
        foreach (GameObject values in childParts)
        {
            if( values.GetComponent<ChildBody>() == null)
            {
                values.AddComponent<ChildBody>();
            }
        }
    }
    public void AssembleAllParts()
    {
        foreach (GameObject values in childParts)
        {
            AssembleIndividualParts(values.GetComponent<ChildBody>());
        }
    }
    public void AssembleIndividualParts(ChildBody _parts)
    {
        _parts.CURRENTSTATE = ChildBody.STATES.ASSEMBLED;
    }    
    public void DismantleAllParts()
    {
        foreach (GameObject values in childParts)
        {
            DismantleIndividualParts(values.GetComponent<ChildBody>());
        }
    }
    public void DismantleIndividualParts(ChildBody _parts)
    {
        _parts.CURRENTSTATE = ChildBody.STATES.DISMANTLED;
    }
}

public class ChildBody : MonoBehaviour
{
    public enum STATES
    {
        ASSEMBLED,
        DISMANTLED
    };

    [HideInInspector]
    public Vector3 initialPosition;

    [HideInInspector]
    public Quaternion initialRotation;


    private STATES currentstate;

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
