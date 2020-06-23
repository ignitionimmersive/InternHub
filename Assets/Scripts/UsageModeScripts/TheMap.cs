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
    public GameObject theLens;

    [SerializeField] GameObject workBench;
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
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(0);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                }

                else if (hit.collider.gameObject == button02)
                {
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(1);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                    //workBench.GetComponent<Animator>().enabled = false;
                }
                else if (hit.collider.gameObject == button03)
                {
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(2);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                }
                else if (hit.collider.gameObject == button04)
                {
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(3);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                }
                else if (hit.collider.gameObject == button05)
                {
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(4);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                }
                else if (hit.collider.gameObject == button06)
                {
                    theLens.SetActive(true);
                    UsageMode01.AssignVideo(5);
                    workBench.GetComponent<Animator>().SetInteger("MapController", 0);
                    this.gameObject.SetActive(false);
                }

                
            }
        }
    }
}

