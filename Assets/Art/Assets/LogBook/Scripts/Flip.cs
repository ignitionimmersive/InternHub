using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Flip : MonoBehaviour
{
    private enum ButtonType
    {
        NextButton,
        PrevButton
    }

    [SerializeField] Button prevButton;
    [SerializeField] Button nextButton; 


    private Vector3 rotationVector;
    private Quaternion startRotation;
    private Vector3 startPosition;
    private bool nextClick;
    private bool previousClick;

    private DateTime startTime;
    private DateTime endTime;

    public GameObject thisthePlane;
    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(() =>NextButton_Click());
        }
        if (prevButton != null)
        {
            prevButton.onClick.AddListener(() => PrevButton_Click());
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {

                if (touch.phase == TouchPhase.Ended)
                {
                    if (hit.collider.gameObject.name == "PlaneNext")
                {
                        hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    
                         transform.Rotate(rotationVector * Time.deltaTime);
                            endTime = DateTime.Now;
                        this.gameObject.transform.position = new Vector3(0,0,0);


                        if (nextButton)
                        {
                            if ((endTime - startTime).TotalSeconds >= 1)
                            {
                                nextClick = false;

                                Vector3 newRotation = new Vector3(0,0,180);
                                transform.rotation = Quaternion.Euler(newRotation);
                            }

                        }    
                        


                    }
                }
            }
        }
    }

        public void nextPage()
    {
        Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), this.gameObject.transform);
    }

    
    public void NextButton_Click()
    {
        nextClick = true;
        startTime = DateTime.Now;
        nextButton.gameObject.SetActive(true);
        //rotationVector = new Vector3(0, 0, 180);
    }
    private void PrevButton_Click()
    {
        previousClick = true;
        startTime = DateTime.Now;
        prevButton.gameObject.SetActive(true);
        rotationVector = new Vector3(0, 0, 180);
        
    }











}
 