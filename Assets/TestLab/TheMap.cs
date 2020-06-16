using System.Collections.Generic;
using UnityEngine;

public class TheMap : MonoBehaviour
{
    public GameObject button01;
    public GameObject button02;
    public GameObject button03;
    public GameObject button04;
    public GameObject button05;
    public GameObject button06;
    public UsageMode UsageMode01;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == button01)
                {
                    UsageMode01.AssignVideo(0);
                }

                else if (hit.collider.gameObject == button02)
                {
                    UsageMode01.AssignVideo(1);
                }
                else if (hit.collider.gameObject == button03)
                {
                    UsageMode01.AssignVideo(2);
                }
                else if (hit.collider.gameObject == button04)
                {
                    UsageMode01.AssignVideo(3);
                }
                else if (hit.collider.gameObject == button05)
                {
                    UsageMode01.AssignVideo(4);
                }
                else if (hit.collider.gameObject == button06)
                {
                    UsageMode01.AssignVideo(5);
                }
            }
        }
    }
}


[System.Serializable]
public class TheMapButton
{
    public GameObject theButton;
}
