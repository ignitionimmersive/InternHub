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
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            GameObject open = hit.collider.gameObject;

            if (open.CompareTag("Player"))
            {
                debug.text = "FOUND";
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
            if (info == panel) panel.OpenPanel();
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
