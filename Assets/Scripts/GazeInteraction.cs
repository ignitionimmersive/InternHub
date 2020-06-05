using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class GazeInteraction : MonoBehaviour
{
    private List<InfoPanel> panels = new List<InfoPanel>();
    public Text debug;
    public Button select;

    void Start()
    {
        panels = FindObjectsOfType<InfoPanel>().ToList();
    }

    void Update()
    {
        // Disable the button til user points to the virtual button.
        select.gameObject.SetActive(false);

        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        debug.text = "Not found";

        if (Physics.Raycast (ray, out RaycastHit hit))
        {
            GameObject open = hit.collider.gameObject;

            if (open.CompareTag("Mechanic"))
                OpenPanel(open.GetComponent<InfoPanel>());
            else if (open.CompareTag("MechanicButton"))
            {
                // Break the model.
                debug.text = "FOUND";
                select.gameObject.SetActive(true);
            }
            else
                CloseAll();
        }
    }

    private void OpenPanel(InfoPanel info)
    {
        foreach (InfoPanel panel in panels)
        {
            if (info == panel)
                info.OpenPanel();
            else
                info.ClosePanel();
        }
    }

    private void CloseAll()
    {
        foreach (InfoPanel panel in panels)
            panel.ClosePanel();
    }
}
