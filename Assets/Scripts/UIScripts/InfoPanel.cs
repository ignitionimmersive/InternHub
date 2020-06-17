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

    public List<Transform> panels = new List<Transform>();

    Vector3 defaultScale = Vector3.zero;
    Vector3 desiredScale; 

    private void Start()
    {
        panels.Add(mechanicPanel);
        panels.Add(usagePanel);
        panels.Add(learnPanel);
        panels.Add(placePanel);

        foreach (Transform panel in panels)
            desiredScale = panel.transform.localScale;
    }

    void Update()
    {
        foreach (Transform panel in panels)
        {
            panel.localScale = Vector3.Lerp(panel.localScale, defaultScale, Time.deltaTime * speed);
        }
    }

    public void OpenPanel() => defaultScale = desiredScale;

    public void ClosePanel() => defaultScale = Vector3.zero; 
}
