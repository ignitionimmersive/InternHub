using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.CompilerServices;

public class UIBehaviour : MonoBehaviour
{
    public Text debug;
    public Material hitColor;

    [HideInInspector]
    public bool isLearningActive;

    [HideInInspector]
    public bool isPlaceModeActive;

    [HideInInspector]
    public bool isUseModeActive;

    private List<GameObject> panels = new List<GameObject>();

    void Update()
    {
        debug.text = "Not found";
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;

                if (open.CompareTag("MechanicPanel"))
                {
                    debug.text = open.name;
                    open.GetComponent<MeshRenderer>().material = hitColor;
                    StartCoroutine(disableUI());

                    // Mechanic.
                }
                else if (open.CompareTag("UsagePanel"))
                {
                    // Usage mode.
                    debug.text = open.name;
                    isUseModeActive = true;
                }
                else if (open.CompareTag("LearnPanel"))
                {
                    // Learn mode.
                    debug.text = open.name;
                    isLearningActive = true;
                }
                else if (open.CompareTag("PlacePanel"))
                {
                    debug.text = open.name;
                    // Place mode.
                    isPlaceModeActive = true;
                }
            }
        }
    }

    IEnumerator disableUI()
    {
        panels.Add(GameObject.FindGameObjectWithTag("MechanicPanel"));
        panels.Add(GameObject.FindGameObjectWithTag("UsagePanel"));
        panels.Add(GameObject.FindGameObjectWithTag("LearnPanel"));
        panels.Add(GameObject.FindGameObjectWithTag("PlacePanel"));

        foreach (GameObject panel in panels)
            panel.SetActive(false);

        if (isLearningActive || isPlaceModeActive || isUseModeActive)
        {
            var asset = GameObject.FindGameObjectWithTag("Player");
            asset.SetActive(false);
        }
        
        yield return new WaitForSeconds(5f);
    }
}
