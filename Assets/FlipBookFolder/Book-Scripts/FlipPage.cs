using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class FlipPage : MonoBehaviour
{
    private enum ButtonType
    {
        NextButton,
        PrevButton
    }

    [SerializeField] Button closeButton;
    [SerializeField] Button prevButton;
    [SerializeField] Button nextButton;

    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip openBookAudio = null;

    [SerializeField] TMP_Text Text1_1;
    [SerializeField] TMP_Text Text1_2;
    [SerializeField] TMP_Text Text2_1;
    [SerializeField] TMP_Text Text2_2;
    [SerializeField] Image img1;
    [SerializeField] Image img2;
    [SerializeField] Image img3;
    [SerializeField] Image img4;




    private Vector3 rotationVector;
    private Quaternion startRotation;
    private Vector3 startPosition;
    private bool isClicked;

    private DateTime startTime;
    private DateTime endTime;
    // Start is called before the first frame update
    private void Start()
    {
        startRotation = transform.rotation;
        startPosition = transform.position;
        if(closeButton != null)
        {
            closeButton.onClick.AddListener(() => closeButton_Click());
        }
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(() => turnOnePageButton_Click(ButtonType.NextButton));
        }
        if (prevButton != null)
        {
            prevButton.onClick.AddListener(() => turnOnePageButton_Click(ButtonType.PrevButton));
        }

    }

    private void Awake()
    {
        AppEvent.OpenBook += new EventHandler(openButton_Click);
    }

    // Update is called once per frame
    private void Update()
    {
       if(isClicked)
       {
            transform.Rotate(rotationVector * Time.deltaTime);
            endTime = DateTime.Now;
            if((endTime - startTime).TotalSeconds >=1)
            {
                isClicked = false;
                transform.position = startPosition;
                transform.rotation = startRotation;

                SetVisibleText();
            }
            

        }
    }

    private void turnOnePageButton_Click(ButtonType type)
    {
        isClicked = true;
        startTime = DateTime.Now;
        nextButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);

        if(type == ButtonType.NextButton)
        {
            rotationVector = new Vector3(0, 180, 0);
            SetFlipPageText(PageClass.CurrentPage2, PageClass.CurrentPage2 + 1);

            PageClass.CurrentPage1 += 2;
            PageClass.CurrentPage2 += 2;

            PageClass pge = PageClass.RandomPage;

            if((PageClass.CurrentPage1 >= pge.Pages.Count) || (PageClass.CurrentPage2 >= pge.Pages.Count))
            {
                nextButton.gameObject.SetActive(false);
            }
        }
        else if(type == ButtonType.PrevButton)
        {
            Vector3 newRotation = new Vector3(startRotation.x, 180, startRotation.y);
            transform.rotation = Quaternion.Euler(newRotation);
            rotationVector = new Vector3(0, -180, 0);

            SetFlipPageText(PageClass.CurrentPage1 - 1, PageClass.CurrentPage1);

            PageClass.CurrentPage1 -= 2;
            PageClass.CurrentPage2 -= 2;

            if ((PageClass.CurrentPage1 <= 0) || (PageClass.CurrentPage2 <= 0))
            {
                prevButton.gameObject.SetActive(false);
            }
        }
        PlaySound();

    }
    private void openButton_Click(object sender, EventArgs e)
    {
        PageClass pge = PageClass.GetRandomPage();
        PageClass.CurrentPage1 = 0;
        PageClass.CurrentPage2 = 1;

        nextButton.gameObject.SetActive(false);
        prevButton.gameObject.SetActive(false);

        if(pge.Pages.Count > 2)
        {
            nextButton.gameObject.SetActive(true);
        }

        SetVisibleText();
    }

    private void SetVisibleText()
    {
        PageClass pge = PageClass.RandomPage;

        string body1 = "";
        string body2 = "";

        if(PageClass.CurrentPage1 < pge.Pages.Count)
        {
            body1 = pge.Pages[PageClass.CurrentPage1];
        }
        if (PageClass.CurrentPage2 < pge.Pages.Count)
        {
            body2 = pge.Pages[PageClass.CurrentPage2];
        }

        Text1_1.text = body1;
        Text1_2.text = body2;
    }

    private void SetFlipPageText(int leftPage, int rightPage)
    {
        PageClass pge = PageClass.RandomPage;

        string bodyRight = "";
        string bodyLeft = "";

        if (rightPage < pge.Pages.Count)
        {
            bodyRight = pge.Pages[rightPage];
        }
        if (leftPage < pge.Pages.Count)
        {
            bodyLeft = pge.Pages[leftPage];
        }

        Text2_1.text = bodyLeft;
        Text2_2.text = bodyRight;
    }

    private void closeButton_Click()
    {
        AppEvent.ClosedBookFun();
    }


    private void PlaySound()
    {
        if ((audioSource != null) && (openBookAudio != null))
        {
            audioSource.PlayOneShot(openBookAudio);
        }
    }


    
}
