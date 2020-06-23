﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] GameObject exitPlace;
    private Vector3 startPos;
    private Quaternion startRot;
    private float threshold = 0.2f;

    public UIBehaviour appState;
    public Text debug;

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
                    spitfire.gameObject.SetActive(false);
                    scope.SetActive(false);
                    exitPlace.SetActive(false);
                }
            }
        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            GameObject _scope = hit.collider.gameObject;
            
            if (_scope.CompareTag("Player"))
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
                        _scope.transform.parent = spitfire;
                        _scope.transform.rotation = spitfire.rotation;

                        // Move the scope to its correct position.
                        this.GetComponent<MoveToAPoint>();
                        this.GetComponent<MoveToAPoint>().finalPosition = spitfire.position;
                        this.GetComponent<MoveToAPoint>().moveSpeed = 1f;
                        this.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;

                        // If it is in absolute position then plane will take off.
                        if (this.GetComponent<MoveToAPoint>().CURRENTSTATE == MoveToAPoint.MOVE_TO_A_POINT_STATE.FINAL_POSITION)
                            spitfire.gameObject.GetComponent<Animator>().enabled = true;
                    }
                }
            }
        }
    }
}
