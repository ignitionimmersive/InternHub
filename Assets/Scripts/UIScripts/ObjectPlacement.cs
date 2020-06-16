using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectPlacement : MonoBehaviour
{
    private Vector3 startPos;
    public Text debug;

    [SerializeField] Transform destination;
    [SerializeField] GameObject scope;

    private void Start()
    {
        startPos = scope.transform.position;
    }

    void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject _scope = hit.collider.gameObject;
       
            if (_scope.CompareTag("Player"))
            {
                Debug.Log("HERE");
                _scope.transform.parent = Camera.main.transform;
                debug.text = "Attached";

                if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    _scope.transform.parent = null;

                    if (_scope.transform.position != destination.position)
                    {
                        _scope.transform.position = startPos;
                        debug.text = "Drop";
                    }
                    //else
                        // Trigger Animation - Plane Taking off.
                }
            }
        }
    }
}
