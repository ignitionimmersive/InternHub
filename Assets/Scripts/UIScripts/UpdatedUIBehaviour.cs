using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum ActiveMode {INITIAL, MAIN, USAGE, BUILD, PLACE, LEARN }

public class UpdatedUIBehaviour : MonoBehaviour
{
    private static ActiveMode activeMode = ActiveMode.INITIAL;

    public GameObject mainContent;
   
    public static UpdatedUIBehaviour Instance { get; set; }

    public ActiveMode CurrentMode
    {
        get
        {
            return activeMode;
        }
        set
        {
            activeMode = value;
        }
    }
    
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
    [SerializeField] GameObject InstructionsDismantle;
    [SerializeField] GameObject InstructionsBuild;

    //Place Mode
    [SerializeField] GameObject SmallScope;
    [SerializeField] GameObject Spitfire;

    // Use mode
    [SerializeField] UsageController UseModeController;
   // [SerializeField] GameObject theLens;
    [SerializeField] Collider MapCollider;

    //learn mode
    [SerializeField] GameObject Logbook;
    [SerializeField] GameObject LogbookBuildings;

   
    //------------//

    #region Private Functions
    private void Start()
    {
        StatesSet(activeMode);
    }

    public void StatesSet(ActiveMode mode)
    {
        switch(mode)
        {
            case ActiveMode.INITIAL:
                {
                    SpawningObject.Instance.IsPlaced = false;
                    SpawningObject.Instance.debug.text = "Initial mode";

                    if (this.mainContent.gameObject != null)
                    {
                        Destroy(this.mainContent.gameObject);
                    }

                    SpawningObject.Instance.debug.text = "Placing..";

                    break;
                }
            case ActiveMode.MAIN:
                {
                    activeMode = ActiveMode.MAIN;

                    Spitfire.GetComponent<Animator>().enabled = false;

                    BigScope.gameObject.SetActive(true);
                    Blueprint.gameObject.SetActive(false);
                    BuildModeController.enabled = (false);
                    InstructionsBuild.SetActive(false);
                    InstructionsDismantle.SetActive(false);

                    //SmallScope.transform.position = smallScopeLocation;
                    SmallScope.SetActive(false);
                    Spitfire.SetActive(false);

                   // theLens.SetActive(false);
                    MapCollider.enabled = (false);
                    UseModeController.enabled = false;

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

                    activeMode = ActiveMode.BUILD;

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

                    activeMode = ActiveMode.USAGE;

                   // theLens.SetActive(false);
                    MapCollider.enabled = (false);
                    UseModeController.enabled = true;
                    UseModeController.StartMode();

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

                    activeMode = ActiveMode.LEARN;

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

                    activeMode = ActiveMode.PLACE;

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
        StatesSet(CurrentMode);

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;


                if (open.CompareTag("GoBack"))
                {
                     if (activeMode == ActiveMode.LEARN)
                     {
                        ExitLearn();
                     }

                     if (activeMode == ActiveMode.USAGE)
                     {
                        ExitUse();
                     }
                    
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

        if (activeMode == ActiveMode.BUILD)
        {
            if (BuildModeController.theParent.CURRENT_STATE != TheParent.PARENT_STATE.ALL_CHILD_ON_BODY)
            {

                exitBuild.SetActive(false);

                if (BuildModeController.theParent.CURRENT_STATE == TheParent.PARENT_STATE.ALL_CHILD_ON_BLUEPRINT)
                {
                    InstructionsDismantle.SetActive(false);
                    InstructionsBuild.SetActive(true);
                }
                else
                {
                    InstructionsDismantle.SetActive(false);
                    InstructionsBuild.SetActive(false);
                }
            }
            else
            {
                InstructionsBuild.SetActive(false);
                InstructionsDismantle.SetActive(true);
                exitBuild.SetActive(true);
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

    void ExitUse()
    {
        UseModeController.ExitMode();
    }

    #endregion

    //------------//

    #region Public Functions

    #endregion
}
