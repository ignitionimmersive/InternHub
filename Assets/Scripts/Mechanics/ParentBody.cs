using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ParentBody : MonoBehaviour
{
    public enum STATES 
    { 
        ASSEMBLED, 
        DISMANTLED 
    };

    public TheBlueprint theBlueprint;

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


    public void MoveAllPartsToBlueprint()
    {
        foreach (GameObject values in childParts)
        {
            MoveIndividualPartToBlueprint(values.GetComponent<ChildBody>());
        }
    }
    public void MoveIndividualPartToBlueprint(ChildBody _parts)
    {
        _parts.CURRENTSTATE = ChildBody.STATES.MOVING_TO_BLUEPRINT;
    }
}

