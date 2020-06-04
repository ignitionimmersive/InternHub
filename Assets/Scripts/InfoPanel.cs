using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{
    const float speed = 6.0f;

    [SerializeField] Transform infoPanel;

    Vector3 scale = Vector3.zero;

    void Update()
    {
        infoPanel.localScale = Vector3.Lerp(infoPanel.localScale, scale, Time.deltaTime * speed);
    }

    public void OpenPanel()
    {
        scale = Vector3.one;
    }

    public void ClosePanel()
    {
        scale = Vector3.zero;
    }
}
