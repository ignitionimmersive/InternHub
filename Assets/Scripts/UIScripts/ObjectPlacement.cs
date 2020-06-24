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
    private bool isPlaced;

    public UIBehaviour appState;
    public Text debug;

    [SerializeField] GameObject exitPlace;
    [SerializeField] Transform spitfire;
    [SerializeField] GameObject scope;

    private void Start()
    {
        startPos = scope.transform.position;
        startRot = scope.transform.rotation;
    }

    public void ActivatePlacement()
    {
        if (appState.isPlaceModeActive == false)
            return;

        spitfire.gameObject.SetActive(true);
        scope.SetActive(true);

        if (Input.touchCount > 0 && Input.GetTouch(Input.touchCount - 1).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(Input.touchCount - 1).position);
            if (Physics.Raycast(ray, out RaycastHit hits))
            {
                if (hits.collider.gameObject.CompareTag("GoBack"))
                {
                    appState.isPlaceModeActive = false;

                    // Deactive spifire.
                    spitfire.gameObject.SetActive(false);
                    spitfire.gameObject.GetComponent<Animator>().enabled = false;

                    // Deactive small scope.
                    scope.SetActive(false);
                    scope.transform.parent = spitfire;
                    scope.transform.rotation = spitfire.rotation;
                    this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.INITIAL_POSITION;

                    exitPlace.SetActive(false);
                    appState.theScope.SetActive(true);
                }
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            GameObject _scope = hit.collider.gameObject;
            
            if (_scope.CompareTag("Player") && !isPlaced)
            {
                _scope.transform.parent = Camera.main.transform;

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _scope.transform.parent = null;

                    if (Vector3.Distance(_scope.transform.position, spitfire.position) > threshold)
                    {
                        _scope.transform.position = startPos;
                        _scope.transform.rotation = startRot;
                        debug.text = "Drop";
                    }
                    else
                    {
                        debug.text = "Correct";
                        isPlaced = true;
                        _scope.transform.parent = spitfire;
                        _scope.transform.rotation = spitfire.rotation;

                        // Move the scope to its correct position.
                        this.gameObject.AddComponent<MoveToAPoint>();
                        this.gameObject.GetComponent<MoveToAPoint>();
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
