using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//this is the main object that contains a list individual parts
public class MainBody : MonoBehaviour
{

    public List<SubPart> subParts;

    void Start()
    {
        //remove rigidbodies from all objects
        foreach (SubPart values in subParts)
        {
            Destroy(values.subPartElement.GetComponent<Rigidbody>());
        }
    }
    public void InitialiseInitialPositionTransform(List<SubPart> _subPart)
    {
        foreach (SubPart values in subParts)
        {
            values.initialPosition = values.subPartElement.GetComponent<Transform>().position;
            values.initialRotation = values.subPartElement.transform.rotation;

        }
    }

    public void DismantleAllParts(List<SubPart> _subPart)
    {

        foreach (SubPart values in subParts)
        {
            DismantleIndividualParts(values);
        }
    }

    public void DismantleIndividualParts(SubPart _part)
    {
        if (_part.subPartElement.GetComponent<Rigidbody>() == null)
        {
            _part.subPartElement.AddComponent<Rigidbody>();
        }

        _part.dismantled = true;

    }

    public void ReAssembleAllParts(List<SubPart> _subPart)
    {
        foreach (SubPart values in subParts)
        {
            ReAssembleIndividualSubPart(values);
        }
    }

    public void ReAssembleIndividualSubPart(SubPart _part)
    {
            Destroy(_part.subPartElement.GetComponent<Rigidbody>());

        _part.dismantled = false;
        _part.subPartElement.transform.position = _part.initialPosition;
        _part.subPartElement.transform.rotation = _part.initialRotation;
    }
}


//this class is for the individual components of the main object
[System.Serializable]
//[RequireComponent(typeof(MainBody))]
public class SubPart
{

    public GameObject subPartElement;

    [HideInInspector]
    public Vector3 initialPosition;

    [HideInInspector]
    public Quaternion initialRotation;

    public bool dismantled
    {
        get; set;
    }
    
}





