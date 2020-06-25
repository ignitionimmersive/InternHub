using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;

    private float threshold = 0.2f;

    private bool _isAttached;

    private Vector3 placeScale = new Vector3(0.2f, 0.2f, 0.2f);

    public UIBehaviour gameController;

    public Text debug;

    public Transform spitfire;

    public bool isAttached
    {
        get { return _isAttached;  }
        set { }
    }

    private void Start()
    {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
    }

    public void ActivatePlacement()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            this.transform.parent = Camera.main.transform;
            this.transform.localScale = placeScale;

            _isAttached = true;
        }
    }

    public void PlacementProcessing()
    {
        //debug.text = "Placing";
        if (Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
        {
            this.transform.parent = null;
            _isAttached = false;

            if (Vector3.Distance(this.transform.position, spitfire.position) > threshold)
            {
                this.transform.position = startPos;
                this.transform.rotation = startRot;
                debug.text = "Drop";
                //isAttached = false;
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
            }
        }
    }
}
