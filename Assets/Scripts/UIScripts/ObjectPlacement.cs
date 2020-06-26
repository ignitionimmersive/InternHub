using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    private float threshold = 0.2f;

    private bool _isAttached;

    private Vector3 placeScale = new Vector3(0.01f, 0.01f, 0.01f);

    public Text debug;

    public Transform spitfire;

    public Transform mainContent;

    [HideInInspector]
    public bool readyToExit;

    public bool isAttached
    {
        get { return _isAttached;  }
        set { }
    }

    public void ActivatePlacement()
    {
        foreach (GameObject value in this.GetComponent<TheParent>().children)
        {
            value.gameObject.GetComponent<TheChild>().initialPosition = value.gameObject.transform.position;
            value.gameObject.GetComponent<TheChild>().initialRotation = value.gameObject.transform.rotation;
        }

        this.transform.parent = Camera.main.transform;
        this.transform.localScale = placeScale;

        _isAttached = true;
    }

    public void PlacementProcessing()
    {
        if (Vector3.Distance(this.transform.position, spitfire.position) > threshold)
        {
            foreach (GameObject value in this.GetComponent<TheParent>().children)
            {
                value.transform.position = value.GetComponent<TheChild>().initialPosition;
                value.transform.rotation = value.GetComponent<TheChild>().initialRotation;
            }
            
            debug.text = this.transform.position.ToString();

            _isAttached = false;
        }
        else
        {
            debug.text = "Correct";
            this.transform.parent = spitfire;
            this.transform.rotation = spitfire.rotation;

            // Move the scope to its correct position.
            this.gameObject.AddComponent<MoveToAPoint>();
            this.gameObject.GetComponent<MoveToAPoint>().finalPosition = spitfire.position;
            this.gameObject.GetComponent<MoveToAPoint>().moveSpeed = 1f;
            this.gameObject.GetComponent<MoveToAPoint>().timeToStart = 0.001f;
            this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;

            // If it is in absolute position then plane will take off.
            spitfire.gameObject.GetComponent<Animator>().enabled = true;

            readyToExit = true;
        }
    }
}
