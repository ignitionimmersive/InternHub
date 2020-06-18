using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    private Vector3 startPos;
    private Quaternion startRot;
    private float threshold = 0.05f;
    private bool isPlaced = false;

    public UIBehaviour status;
    public Text debug;

    [SerializeField] Transform destination;
    [SerializeField] GameObject scope;

    private void Start()
    {
        startPos = scope.transform.position;
        startRot = scope.transform.rotation;
    }

    void Update()
    {
        if (isPlaced || status.isPlaceModeActive)
            return;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject _scope = hit.collider.gameObject;

            debug.text = Vector3.Distance(_scope.transform.position, destination.position).ToString();
            if (_scope.CompareTag("Player"))
            {
                //Debug.Log("HERE");
                _scope.transform.parent = Camera.main.transform;
                //debug.text = "Attached";

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _scope.transform.parent = null;

                    if (Vector3.Distance(_scope.transform.position, destination.position) > threshold)
                    {
                        _scope.transform.position = startPos;
                        debug.text = "Drop" + " " + Vector3.Distance(_scope.transform.position, destination.position).ToString();
                    }
                    else
                    {
                        debug.text = "Correct" + " " + Vector3.Distance(_scope.transform.position, destination.position).ToString();
                        isPlaced = true;
                        // Trigger Animation - Plane Taking off.
                    }
                }
            }
        }
    }
}
