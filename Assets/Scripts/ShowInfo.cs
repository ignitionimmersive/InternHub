using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ShowInfo : MonoBehaviour
{
    private List<InfoPanel> panels = new List<InfoPanel>();

    void Start()
    {
        panels = FindObjectsOfType<InfoPanel>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject open = hit.collider.gameObject;

            if (open.CompareTag("Mechanic"))
            {
                //debug.text = panels.Count.ToString();
                OpenPanel(open.GetComponent<InfoPanel>());
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
