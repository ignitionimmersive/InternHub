using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

//this is the main object that contains a list individual parts
public class MainBody : MonoBehaviour
{
    public enum STATES { ASSEMBLED, DISMANTLED, CHANGING };

    public STATES CURRENTSTATE;

    public int totalSubParts;

    public int totalPartsDismantled = 0;

    public List<SubPart> subParts;


    void Start()
    {
        InitialiseInitialPositionTransform(subParts);
        totalSubParts = subParts.Count;

    }

    public void InitialiseInitialPositionTransform(List<SubPart> _subPart)
    {
        foreach (SubPart values in subParts)
        {
            Destroy(values.subPartElement.GetComponent<Rigidbody>());
            values.initialPosition = values.subPartElement.GetComponent<Transform>().position;
            values.initialRotation = values.subPartElement.transform.rotation;
        }

        this.CURRENTSTATE = STATES.ASSEMBLED;
    }

    public void DismantleAllParts()
    {
        foreach (SubPart values in subParts)
        {
            DismantleIndividualParts(values);
        }
    }

    public void DismantleIndividualParts(SubPart _part)
    {
        if(_part.CURRENTSTATE != SubPart.STATES.DISMANTLED)
        {
            this.CURRENTSTATE = STATES.DISMANTLED;

            if (_part.subPartElement.GetComponent<Rigidbody>() == null)
            {
                _part.subPartElement.AddComponent<Rigidbody>();
            }
            _part.CURRENTSTATE = SubPart.STATES.DISMANTLED;

            this.totalPartsDismantled++;
        }

    }

    public void ReAssembleAllParts()
    {
        foreach (SubPart values in subParts)
        {
            ReAssembleIndividualSubPart(values);
        }
    }

    public void ReAssembleIndividualSubPart(SubPart _part)
    {
        if (_part.CURRENTSTATE != SubPart.STATES.ASSEMBLED)
        {
            Destroy(_part.subPartElement.GetComponent<Rigidbody>());

            _part.subPartElement.transform.position = _part.initialPosition;
            _part.subPartElement.transform.rotation = _part.initialRotation;

            _part.CURRENTSTATE = SubPart.STATES.ASSEMBLED;
            this.totalPartsDismantled--;

            if (this.totalPartsDismantled <= 0)
            {
                this.CURRENTSTATE = STATES.CHANGING;
                InitialiseInitialPositionTransform(subParts);

            }
        }
    }
}








