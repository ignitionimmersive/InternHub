using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    private List<InfoPanel> panels = new List<InfoPanel>();
    public Text debug;
    public ParentBody prefab;

    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        debug.text = "Not found";

        if (Physics.Raycast (ray, out RaycastHit hit))
        {
            GameObject open = hit.collider.gameObject;

            if (open.CompareTag("MechanicButton"))
            {
                // Mechanic.
                debug.text = "FOUND";
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
