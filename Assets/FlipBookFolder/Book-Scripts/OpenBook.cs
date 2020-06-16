﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARCore;


public class OpenBook : MonoBehaviour
{

    [SerializeField] Button openButton = null;
    [SerializeField] GameObject openedBook = null;
    [SerializeField] GameObject insideBackCover = null;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip openBookAudio = null;

    private Vector3 rotationVector;
    private Quaternion startRotation;
    private bool isOpenClicked;
    private bool isCloseClicked;

    private DateTime startTime;
    private DateTime endTime;

    public bool interactable = true;


    // Start is called before the first frame update
    void Start()
    {
        startRotation = transform.rotation;
        if (openButton != null)
        {
            openButton.onClick.AddListener(() => openButton_Click());
        }

        AppEvent.CloseBook += new EventHandler(closeButton_Click);

    }


    // Update is called once per frame
    void Update()
    {

        if ((isOpenClicked) || (isCloseClicked))
        {
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;

            if (isOpenClicked == true)
            {
                if ((endTime - startTime).TotalSeconds >= 1)
                {
                    isOpenClicked = false;
                    gameObject.SetActive(false);
                    insideBackCover.SetActive(false);
                    openedBook.SetActive(true);

                    AppEvent.OpenBookFun();

                    Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.y);
                    transform.rotation = Quaternion.Euler(newRotation);
                }
            }
            else if (isCloseClicked == true)
            {
                if ((endTime - startTime).TotalSeconds >= 1)
                {
                    isCloseClicked = false;


                    Vector3 newRotation = new Vector3(startRotation.x, 0, startRotation.y);
                    transform.rotation = Quaternion.Euler(newRotation);
                }
            }
        }
    }   
    public void openButton_Click()

    {
            isOpenClicked = true;
            startTime = DateTime.Now;
            rotationVector = new Vector3(0, 180, 0);

            PlaySound();
        
    }
   
    public void closeButton_Click(object sender, EventArgs e)
    {
       
            gameObject.SetActive(true);
            insideBackCover.SetActive(true);
            openedBook.SetActive(false);

            isCloseClicked = true;
            startTime = DateTime.Now;
            rotationVector = new Vector3(0, -180, 0);

            PlaySound();
        
    }

    
    private void PlaySound()
    {
        if((audioSource != null) && (openBookAudio != null))
        {
            audioSource.PlayOneShot(openBookAudio);
        }
    }
   
}
