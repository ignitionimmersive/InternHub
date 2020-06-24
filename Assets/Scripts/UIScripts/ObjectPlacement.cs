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
    private bool isAttached;

    public UIBehaviour appState;
    public Text debug;

    [SerializeField] Transform spitfire;

    private void Start()
    {
        startPos = this.transform.position;
        startRot = this.transform.rotation;
    }

    private void Update()
    {
        //debug.text = Input.touchCount.ToString();

        if (appState.isPlaceModeActive == false)
        {
            debug.text = "Not Working";
            return;
        }

        if (isAttached)
        {
            PlacementProcessing();
        }
        else
        {
            ActivatePlacement();
        }
    }

    public void ActivatePlacement()
    {
        spitfire.gameObject.SetActive(true);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                debug.text = "FOUND";
                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began && !isAttached)
                {
                    this.transform.parent = Camera.main.transform;
                    isAttached = true;
                    //debug.text = "ATTACHED";
                }
            }
        }
    }

    private void PlacementProcessing()
    {
        //debug.text = "Placing";
        if (Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("GoBack"))
                {
                    // Deactive spifire.
                    spitfire.gameObject.GetComponent<Animator>().enabled = false;
                    spitfire.gameObject.SetActive(false);

                    // Deactive small scope.
                    this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.INITIAL_POSITION;
                    this.gameObject.SetActive(false); ;

                    // Make UI buttons persist.
                    if (appState.arCamera.GetComponent<ShowInfo>() != null)
                        Destroy(appState.arCamera.GetComponent<ShowInfo>());

                    appState.isPlaceModeActive = false;
                }
                else
                {
                    debug.text = "Placed!";
                    this.transform.parent = null;

                    if (Vector3.Distance(this.transform.position, spitfire.position) > threshold)
                    {
                        this.transform.position = startPos;
                        this.transform.rotation = startRot;
                        debug.text = "Drop";
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
                        if (this.GetComponent<MoveToAPoint>().CURRENTSTATE == MoveToAPoint.MOVE_TO_A_POINT_STATE.FINAL_POSITION)
                            spitfire.gameObject.GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
    }
}
