using System.Collections;
using System.Collections.Generic;
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
                    modeButtons.SetActive(true);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.BUILD:
                {
                    modeButtons.SetActive(false);
                    exitBuild.SetActive(true);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.USAGE:
                {
                    modeButtons.SetActive(false);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(false);
                    exitUse.SetActive(true);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.LEARN:
                {
                    modeButtons.SetActive(false);
                    exitBuild.SetActive(false);
                    exitLearn.SetActive(true);
                    exitUse.SetActive(false);
                    exitPlace.SetActive(false);
                    break;
                }
            case ActiveMode.PLACE:
                {
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

                if (open.CompareTag("MechanicPanel"))
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
    #endregion

    //------------//

    #region Public Functions

    #endregion
}
