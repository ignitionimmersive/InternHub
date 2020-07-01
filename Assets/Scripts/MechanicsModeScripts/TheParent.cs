using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheParent : MonoBehaviour
{
    public enum PARENT_STATE
    {
        ALL_CHILD_ON_BODY,
        DISMANTLED,
        ALL_CHILD_ON_BLUEPRINT
    };

    public PARENT_STATE CURRENT_STATE;

    public List<GameObject> children;
    //public TheBlueprint theBlueprint;

    public TheBlueprint theBlueprint;

    public int child_on_body_count;
    public int child_on_blueprint_count;

    private void Start()
    {
        InitialiseChildBodyComponents();

    }

    private void Update()
    {
        if (this.child_on_body_count >= this.children.Count)
        {
            this.CURRENT_STATE = PARENT_STATE.ALL_CHILD_ON_BODY;
        }

        else if (this.child_on_blueprint_count >= this.children.Count)
        {
            this.CURRENT_STATE = PARENT_STATE.ALL_CHILD_ON_BLUEPRINT;
        }

        else
        {
            this.CURRENT_STATE = PARENT_STATE.DISMANTLED;
        }
    }

    private void InitialiseChildBodyComponents()
    {
        foreach (GameObject child in children)
        {
            if (child.gameObject.GetComponent<TheChild>() == null)
            {
                child.gameObject.AddComponent<TheChild>();
            }

        }

        this.child_on_body_count = this.children.Count;
    }

    public void DismantleAllChildren()
    {
        foreach( GameObject child in children)
        {
            child.gameObject.GetComponent<TheChild>().CURRENTSTATE = TheChild.CHILD_STATES.DISMANTLE;
        }
    }

    public void AssembleAllChildren()
    {
        foreach (GameObject child in children)
        {
            child.gameObject.GetComponent<TheChild>().CURRENTSTATE = TheChild.CHILD_STATES.MOVE_TO_INITIAL_ASSEMBLY;
        }
    }

}

