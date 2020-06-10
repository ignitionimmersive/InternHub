using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    const float speed = 6.0f;

    [SerializeField] Transform mechanicPanel;
    [SerializeField] Transform usagePanel;
    [SerializeField] Transform learnPanel;
    [SerializeField] Transform placePanel;

    private static List<Transform> panels = new List<Transform>();

    Vector3 scale = Vector3.zero;

    private void Start()
    {
        panels.Add(mechanicPanel);
        panels.Add(usagePanel);
        panels.Add(learnPanel);
        panels.Add(placePanel);
    }

    void Update()
    {
        foreach (Transform panel in panels)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, scale, Time.deltaTime * speed);
        }
    }

    public void OpenPanel() => scale = Vector3.one;

    public void ClosePanel() => scale = Vector3.zero; 
}
