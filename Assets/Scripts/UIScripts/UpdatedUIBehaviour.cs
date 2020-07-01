using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum ActiveMode { MAIN, USAGE, BUILD, PLACE, LEARN }

public class UpdatedUIBehaviour : MonoBehaviour
{
    [HideInInspector] public ActiveMode activeMode = ActiveMode.MAIN;

    [Header ("Mode Button Object")]
    [SerializeField] GameObject modeButtons;

    // Exit buttons.
    [SerializeField] GameObject exitBuild;
    [SerializeField] GameObject exitPlace;
    [SerializeField] GameObject exitLearn;
    [SerializeField] GameObject exitUse;

    // BuildMode 
    [SerializeField] MechanicsController BuildModeController;
    [SerializeField] TheParent BigScope;
    [SerializeField] TheBlueprint Blueprint;

    //Place Mode
    [SerializeField] GameObject SmallScope;
    [SerializeField] GameObject Spitfire;

    // Use mode
    [SerializeField] GameObject theLens;
    [SerializeField] Collider MapCollider;

    //learn mode
    [SerializeField] GameObject Logbook;
    [SerializeField] GameObject LogbookBuildings;




    //------------//

    #region Private Functions
    private void Start()
    {
        StatesSet(ActiveMode.MAIN);
    }

    private void StatesSet(ActiveMode mode)
    {
        switch(mode)
        {
            case ActiveMode.MAIN:
                {
                    
                    BigScope.gameObject.SetActive(true);
                    Blueprint.gameObject.SetActive(false);
                    BuildModeController.enabled = (false);

                    SmallScope.SetActive(false);
                    Spitfire.SetActive(false);

                    theLens.SetActive(false);
                    MapCollider.enabled = (false);

                    Logbook.SetActive(false);
                    LogbookBuildings.SetActive(false);

                    modeButtons.SetActive(true);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.BUILD:
                {
                    BigScope.gameObject.SetActive(true);
                    Blueprint.gameObject.SetActive(true);
                    BuildModeController.enabled = true;


                    modeButtons.SetActive(false);
                    exitBuild.SetActive(true);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.USAGE:
                {
                    

                    theLens.SetActive(true);
                    MapCollider.enabled = (true);

                    BigScope.gameObject.SetActive(false);

                    modeButtons.SetActive(false);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(true);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.LEARN:
                {
                    
                    BigScope.gameObject.SetActive(false);

                    Logbook.SetActive(true);
                    LogbookBuildings.SetActive(true);

                    modeButtons.SetActive(false);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(true);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.PLACE:
                {
                    BigScope.gameObject.SetActive(false);

                    SmallScope.SetActive(true);
                    Spitfire.SetActive(true);

                    

                    modeButtons.SetActive(false);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(true);
                    break;
                }
        }
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;


                if (open.CompareTag("GoBack"))
                {
                    //if (activeMode == ActiveMode.BUILD)
                    
                        
                    

                     if (activeMode == ActiveMode.LEARN)
                    {
                        ExitLearn();
                    }
                    ExitMechanics();
                    StatesSet(ActiveMode.MAIN);
                }
                else if (open.CompareTag("MechanicPanel"))
                {
                    StatesSet(ActiveMode.BUILD);
                }
                else if (open.CompareTag("UsagePanel"))
                {
                    StatesSet(ActiveMode.USAGE);
                }
                else if (open.CompareTag("LearnPanel"))
                {
                    StatesSet(ActiveMode.LEARN);
                }
                else if (open.CompareTag("PlacePanel"))
                {
                    StatesSet(ActiveMode.PLACE);
                }
                
                

            }
        }
    }

    void ExitLearn()
    {
        Logbook.GetComponent<AnimationScript>().CURRENTSTATE = AnimationScript.STATES.OPEN;

        Logbook.SetActive(false);
        GameObject[] buildings = GameObject.FindGameObjectsWithTag("Building");
        foreach (GameObject building in buildings)
        {
            Debug.Log("CloseBuildings");
            building.SetActive(false);
        }
    }

    void ExitMechanics()
    {
        if (BigScope.CURRENT_STATE != TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
        { 
            BigScope.gameObject.GetComponent<TheParent>().AssembleAllChildren();
        }
    }
    #endregion

    //------------//

    #region Public Functions

    #endregion
}
