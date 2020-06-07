using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public Text debug;

    void Update()
    {
        //var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        debug.text = "Not found";

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject open = hit.collider.gameObject;

                if (open.CompareTag("MechanicButton"))
                {
                    // Mechanic.
                    debug.text = "FOUND";
                    open.GetComponentInParent<TheParent>().DismantleAllChildren();
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
