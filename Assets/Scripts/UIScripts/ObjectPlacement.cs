using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    [SerializeField] GameObject exitPlace;
    private Vector3 startPos;
    private Quaternion startRot;
    private float threshold = 0.2f;
    private bool isPlaced = false;

    public Text debug;

    [SerializeField] Transform destination;
    [SerializeField] GameObject scope;

    private void Start()
    {
        startPos = scope.transform.position;
        startRot = scope.transform.rotation;
        //exitPlace.SetActive(true);
    }

    public void ActivatePlacement()
    {
        if (isPlaced)
            return;

        var button = exitPlace.transform.Find("ExitPlace");
        button.gameObject.SetActive(true);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit))
        {
            GameObject _scope = hit.collider.gameObject;
            
            if (_scope.CompareTag("Player"))
            {
                _scope.transform.parent = Camera.main.transform;
                //debug.text = "Attached";

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _scope.transform.parent = null;

                    if (Vector3.Distance(_scope.transform.position, destination.position) > threshold)
                    {
                        _scope.transform.position = startPos;
                        _scope.transform.rotation = startRot;
                        debug.text = "Drop";
                    }
                    else
                    {
                        debug.text = "Correct";
                        isPlaced = true;
                        // Trigger Animation - Plane Taking off.
                    }
                }
            }
        }
    }
}
