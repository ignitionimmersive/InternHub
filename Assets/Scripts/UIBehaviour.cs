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


    //Sam's part of code for testing
    public TheParent theParent;
    private bool theParentHasBeenSpawned = false;

    public TheBlueprint theBlueprint;
    private bool theBluePrintHasBeenSpawned = false;

    private GameObject theParent01;
    private GameObject theBluePrint01;

    ///ends hers
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

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
            debug.text = "Not found";

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
                Vector3 direction = worldTouchPosition - Camera.main.transform.position;
                
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    GameObject open = hit.collider.gameObject;

                    if (open.CompareTag("MechanicButton"))
                    {
                        if (theParentHasBeenSpawned == false)
                        {
                                    this.theParentHasBeenSpawned = true;
                                    theParent01 = (GameObject)GameObject.Instantiate(this.theParent.gameObject, hit.point, Quaternion.identity);
                             
                        }
                        else if ((theBluePrintHasBeenSpawned == false) && (theParentHasBeenSpawned = true))
                        {
                                    theBluePrint01 = (GameObject)GameObject.Instantiate(this.theBlueprint.gameObject, hit.point, Quaternion.identity);
                                    theBluePrint01.transform.Translate(0, 7, 0);
                                    theBluePrint01.transform.Rotate(-90.0f, 0.0f, 0.0f, Space.Self);
                                    theParent01.GetComponent<TheParent>().theBlueprint = theBluePrint01.GetComponent<TheBlueprint>();
                                    this.theBluePrintHasBeenSpawned = true;
                                
                        }


                        else if ((theBluePrintHasBeenSpawned == true) && (theParentHasBeenSpawned) == true)
                        {
                                if (theParent01.GetComponent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
                                {

                                theParent01.GetComponent<TheParent>().DismantleAllChildren();
                                }

                                else if (theParent01.GetComponent<TheParent>().CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                                {
                                theParent01.GetComponent<TheParent>().AssembleAllChildren();
                                    
                                }

                        }
                    }
                    else if (open.CompareTag("UsageButton"))
                    {
                        // Usage mode.
                    }
                    else if (open.CompareTag("LearnButton"))
                    {
                        // Learn mode.
                    }
                    else if (open.CompareTag("PlaceButton"))
                    {
                        // Place mode.
                    }
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
