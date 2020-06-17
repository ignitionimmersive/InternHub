using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ShowInfo : MonoBehaviour
{
    public Text debug;

    private List<InfoPanel> panels = new List<InfoPanel>();

    void Start()
    {
        panels = FindObjectsOfType<InfoPanel>().ToList();
    }

    // Update is called once per frame
    void Update()
    {
        debug.text = "Not found";
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            GameObject open = hit.collider.gameObject;

            if (open.CompareTag("SubPart"))
            {
                debug.text = open.name;
                OpenPanel(open.GetComponent<InfoPanel>());
            }
        }
        else
            CloseAll();
    }

    private void OpenPanel(InfoPanel info)
    {
        foreach (InfoPanel panel in panels)
        {
            if (info == panel)
            {
                panel.OpenPanel();
                //Debug.Log(panel.transform.localScale.ToString());
            }
            else panel.ClosePanel();
        }
    }

    private void CloseAll()
    {
        foreach (InfoPanel panel in panels)
        {
            panel.ClosePanel();
        }
    }
}
