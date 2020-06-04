using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this class is for the individual components of the main object
[System.Serializable]
//[RequireComponent(typeof(MainBody))]
public class SubPart
{
    public enum STATES { ASSEMBLED, DISMANTLED };

    public STATES CURRENTSTATE
    {
        get;
        set;
    }

    public GameObject subPartElement;

    [HideInInspector]
    public Vector3 initialPosition;

    [HideInInspector]
    public Quaternion initialRotation;

}
