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
        CheckState();
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
        CheckState();
    }

    public void CheckState()
    {
        int i = 0;
        foreach (GameObject values in childParts)
        {
            if (values.GetComponent<ChildBody>().CURRENTSTATE == ChildBody.STATES.ASSEMBLED)
            {
                i++;
            }
        }


        if (i == childParts.Count)
        {
            SetState(STATES.ASSEMBLED);
        }
        else
        {
            SetState(STATES.DISMANTLED);
        }
    }

    private void SetState(STATES _state)
    {
        this.CURRENTSTATE = _state;
    }
}
