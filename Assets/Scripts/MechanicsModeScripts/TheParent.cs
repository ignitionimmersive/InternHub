﻿using System.Collections;
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
            child.gameObject.GetComponent<TheChild>().initialRotation = child.gameObject.transform.rotation;
            child.gameObject.GetComponent<TheChild>().initialPosition = child.gameObject.transform.position;

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

    public void CheckParentState()
    {

        int ALL_CHILD_ON_BODY_count = 0;
        int DISMANTLED_count = 0;
        int ALL_CHILD_ON_BLUEPRINT_count = 0;

        foreach (GameObject child in children)
        {
            if (child.gameObject.GetComponent<TheChild>().CURRENTSTATE == TheChild.CHILD_STATES.INITIAL_ASSEMBLY)
            {
                ALL_CHILD_ON_BODY_count++;
            }
            else if (child.gameObject.GetComponent<TheChild>().CURRENTSTATE == TheChild.CHILD_STATES.ON_BLUEPRINT)
            {
                ALL_CHILD_ON_BLUEPRINT_count++;
            }
            else
            {
                DISMANTLED_count++;
            }

        }

        if (this.children.Count == ALL_CHILD_ON_BODY_count)
        {
            this.CURRENT_STATE = PARENT_STATE.ALL_CHILD_ON_BODY;
        }

        else if (this.children.Count == ALL_CHILD_ON_BLUEPRINT_count)
        {
            this.CURRENT_STATE = PARENT_STATE.ALL_CHILD_ON_BLUEPRINT;
        }
        else
        {
            this.CURRENT_STATE = PARENT_STATE.DISMANTLED;
        }

    }



}

