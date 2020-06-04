using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
