using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;


public class AnimationScript : MonoBehaviour
{

    public GameObject Flip;
    public GameObject ReverseFlip;
    public GameObject Page1_gameObject_1;
    public GameObject Page1_gameObject_2;
    public GameObject Page1_gameObject_3;
    public GameObject Page1_gameObject_4;

    public GameObject PopUpText;

    public GameObject Page2_gameObject_1;
    public GameObject Page2_gameObject_2;
    public GameObject Page2_gameObject_3;
    public GameObject Page2_gameObject_4;


    public GameObject Page3;
    public GameObject Page3_2;
    public GameObject Page3_3;

    private Vector3 scaleChange;

    public enum STATES
    {
      
        OPEN,
        PAGE1, PAGE1_REVERSE1,
        PAGE2, PAGE2_REVERSE2,
        PAGE3, PAGE3_REVERSE3,
        PAGE4, PAGE4_REVERSE4
    };

    public STATES CURRENTSTATE;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {


        anim.GetComponent<Animator>();
        scaleChange = new Vector3(0.1f, 0.1f, 0.1f);

    }
    // Update is called once per frame
    void Update()
    {

        //  switch (CURRENTSTATE)
        //{
        //      case STATES.OPEN:
        //          {
        //              if (Input.GetMouseButtonDown(0))
        //              {
        //                  
        //                 anim.Play("Open");
        //                 this.CURRENTSTATE = STATES.PAGE;
        //             }
        //             break;
        //         }
        //     case STATES.PAGE:
        //         {
        //            if (Input.GetMouseButtonDown(1))
        //             {
        //                 
        //                 anim.Play("Page Flip");
        //
        //                 this.CURRENTSTATE = STATES.PAGE1;
        //             }
        //             break;
        //        }






        // }

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
                    Debug.DrawLine(Camera.main.gameObject.transform.position, hit.collider.gameObject.transform.position, Color.red);

                        switch (CURRENTSTATE)
                        {
                       

                        case STATES.OPEN:

                            {
                                if (hit.collider.gameObject.name == "Flip")
                                {
                                 
                                    if (hit.collider.gameObject.name == "Flip")
                                    {
                                        PopUpText.SetActive(false);
                                        anim.Play("Open");
                                        this.CURRENTSTATE = STATES.PAGE1;
                                    }

                                }


                                break;
                            }


                        case STATES.PAGE1:
                            {

                                if ((hit.collider.gameObject.name == "Flip"))
                                {

                                    anim.Play("Page Flip");

                                    this.CURRENTSTATE = STATES.PAGE2;

                                }


                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {
                                    anim.Play("Close");
                                    this.CURRENTSTATE = STATES.OPEN;
                                }

                                break;
                            }


                        case STATES.PAGE2:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {

                                    anim.Play("Page Flip Reverse");
                                    this.CURRENTSTATE = STATES.PAGE1;

                                }
                                if (hit.collider.gameObject.name == "Flip")
                                {
                                    anim.Play("Page Flip 0");

                                    this.CURRENTSTATE = STATES.PAGE3;

                                }

                                break;
                            }
                        case STATES.PAGE3:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {

                                    anim.Play("Page Flip Reverse 0");
                                    this.CURRENTSTATE = STATES.PAGE2;

                                }
                                if (hit.collider.gameObject.name == "Flip")
                                {
                                    anim.Play("Close");

                                    this.CURRENTSTATE = STATES.OPEN;

                                }
                                break;
                            }






                    }

                }



            }

        }
        while (true)
        {
            if (CURRENTSTATE == STATES.PAGE1)
            {
                //Page 1 GameObjects Scaling ( 0 to 2 )

                //GameObject 1
                Page1_gameObject_1.SetActive(true);
                if (Page1_gameObject_1.transform.localScale.y < 1f && Page1_gameObject_1.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_1.gameObject.transform.localScale += scaleChange;
                }

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                if (Page1_gameObject_2.transform.localScale.y < 1f && Page1_gameObject_2.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_2.gameObject.transform.localScale += scaleChange;
                }

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                if (Page1_gameObject_3.transform.localScale.y < 1f && Page1_gameObject_3.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_3.gameObject.transform.localScale += scaleChange;
                }
                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                if (Page1_gameObject_4.transform.localScale.y < 1f && Page1_gameObject_4.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_4.gameObject.transform.localScale += scaleChange;
                }



                //Page 2 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                if (Page2_gameObject_1.transform.localScale.y > 0.1f && Page2_gameObject_1.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_1.gameObject.transform.localScale -= scaleChange;

                }

                //GameObject 2
                Page2_gameObject_2.SetActive(true);
                if (Page2_gameObject_2.transform.localScale.y > 0.1f && Page2_gameObject_2.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_2.gameObject.transform.localScale -= scaleChange;

                }

                //GameObject 3
                Page2_gameObject_3.SetActive(true);
                if (Page2_gameObject_3.transform.localScale.y > 0.1f && Page2_gameObject_3.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_3.gameObject.transform.localScale -= scaleChange;


                }
                //GameObject 3
                Page2_gameObject_4.SetActive(true);
                if (Page2_gameObject_4.transform.localScale.y > 0.1f && Page2_gameObject_4.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_4.gameObject.transform.localScale -= scaleChange;


                }

            }

            else if (CURRENTSTATE == STATES.PAGE1_REVERSE1)
            {
                // Page 1 GameObjects Scaling ( 0 to 2 ) if Page Reversed

                //GameObject 1
                Page1_gameObject_1.SetActive(true);
                if (Page1_gameObject_1.transform.localScale.y < 1f && Page1_gameObject_1.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_1.gameObject.transform.localScale += scaleChange;
                }

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                if (Page1_gameObject_2.transform.localScale.y < 1f && Page1_gameObject_2.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_2.gameObject.transform.localScale += scaleChange;
                }

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                if (Page1_gameObject_3.transform.localScale.y < 1f && Page1_gameObject_3.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_3.gameObject.transform.localScale += scaleChange;
                }
                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                if (Page1_gameObject_4.transform.localScale.y < 1f && Page1_gameObject_4.transform.localScale.y > -0.1f)
                {
                    Page1_gameObject_4.gameObject.transform.localScale += scaleChange;
                }



            }



            else if (CURRENTSTATE == STATES.PAGE2)
            {
                // Page 1 GameObjects Scaling Back To 0 If The State Changes to Page2

                //GameObject 1
                Page1_gameObject_1.SetActive(true);
                if (Page1_gameObject_1.transform.localScale.y > 0.1f && Page1_gameObject_1.transform.localScale.y < 2f)
                {
                    Page1_gameObject_1.gameObject.transform.localScale -= scaleChange;

                }

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                if (Page1_gameObject_2.transform.localScale.y > 0.1f && Page1_gameObject_2.transform.localScale.y < 2f)
                {
                    Page1_gameObject_2.gameObject.transform.localScale -= scaleChange;

                }

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                if (Page1_gameObject_3.transform.localScale.y > 0.1f && Page1_gameObject_3.transform.localScale.y < 2f)
                {
                    Page1_gameObject_3.gameObject.transform.localScale -= scaleChange;

                }
                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                if (Page1_gameObject_4.transform.localScale.y > 0.1f && Page1_gameObject_4.transform.localScale.y < 2f)
                {
                    Page1_gameObject_4.gameObject.transform.localScale -= scaleChange;
                    // Page1_gameObject_4.SetActive(false);

                }
                // Page 2 GameObjects Scaling ( 0 to 2 )

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                if (Page2_gameObject_1.transform.localScale.y < 1f && Page2_gameObject_1.transform.localScale.y > -0.1f)
                    {
                    Page2_gameObject_1.gameObject.transform.localScale += scaleChange;
                    }

                    //GameObject 2
                    Page2_gameObject_2.SetActive(true);
                    if (Page2_gameObject_2.transform.localScale.y < 1f && Page2_gameObject_2.transform.localScale.y > -0.1f)
                    {
                        Page2_gameObject_2.gameObject.transform.localScale += scaleChange;
                    }

                    //GameObject 3
                    Page2_gameObject_3.SetActive(true);
                    if (Page2_gameObject_3.transform.localScale.y < 1f && Page2_gameObject_3.transform.localScale.y > -0.1f)
                    {
                        Page2_gameObject_3.gameObject.transform.localScale += scaleChange;
                    }
                //GameObject 3
                Page2_gameObject_4.SetActive(true);
                    if (Page2_gameObject_4.transform.localScale.y < 1f && Page2_gameObject_4.transform.localScale.y > -0.1f)
                        {
                        Page2_gameObject_4.gameObject.transform.localScale += scaleChange;
                    }

                //Page 3 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page3.SetActive(true);
                    if (Page3.transform.localScale.y > 0.1f && Page3.transform.localScale.y < 2.1f)
                    {
                        Page3.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 2
                    Page3_2.SetActive(true);
                    if (Page3_2.transform.localScale.y > 0f && Page3_2.transform.localScale.y < 2.1f)
                    {
                        Page3_2.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 3
                    Page3_3.SetActive(true);
                    if (Page3_3.transform.localScale.y > 0f && Page3_3.transform.localScale.y < 2.1f)
                    {
                        Page3_3.gameObject.transform.localScale -= scaleChange;

                    }


                }


                else if (CURRENTSTATE == STATES.PAGE3)
                {


                    //Page 2 GameObjects Scalings To ( 2 to 0 )

                    //GameObject 1
                    Page2_gameObject_1.SetActive(true);
                    if (Page2_gameObject_1.transform.localScale.y > 0.1f && Page2_gameObject_1.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_1.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 2
                    Page2_gameObject_2.SetActive(true);
                    if (Page2_gameObject_2.transform.localScale.y > 0.1f && Page2_gameObject_2.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_2.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 3
                    Page2_gameObject_3.SetActive(true);
                    if (Page2_gameObject_3.transform.localScale.y > 0.1f && Page2_gameObject_3.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_3.gameObject.transform.localScale -= scaleChange;

                    }

                //GameObject 4
                Page2_gameObject_4.SetActive(true);
                if (Page2_gameObject_4.transform.localScale.y > 0.1f && Page2_gameObject_4.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_4.gameObject.transform.localScale -= scaleChange;

                }

                //Page 3 GameObjects Scaling ( 0 to 2 )

                //GameObject 1
                Page3.SetActive(true);
                    if (Page3.transform.localScale.y < 1f && Page3.transform.localScale.y > -0.1f)
                    {
                        Page3.gameObject.transform.localScale += scaleChange;
                    }

                    //GameObject 2
                    Page3_2.SetActive(true);
                    if (Page3_2.transform.localScale.y < 1f && Page3_2.transform.localScale.y > -0.1f)
                    {
                        Page3_2.gameObject.transform.localScale += scaleChange;
                    }

                    //GameObject 3
                    Page3_3.SetActive(true);
                    if (Page3_3.transform.localScale.y < 1f && Page3_3.transform.localScale.y > -0.1f)
                    {
                        Page3_3.gameObject.transform.localScale += scaleChange;
                    }



                }

                else if (CURRENTSTATE == STATES.OPEN)
                {
                    //PopUpText.SetActive(true);
                    //Reverting Back to Scale 0 if the Book is Closed.

                    //GameObject 1
                    Page1_gameObject_1.SetActive(true);
                    if (Page1_gameObject_1.transform.localScale.y > 0.1f && Page1_gameObject_1.transform.localScale.y < 2f)
                    {
                        Page1_gameObject_1.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 2
                    Page1_gameObject_2.SetActive(true);
                    if (Page1_gameObject_2.transform.localScale.y > 0.1f && Page1_gameObject_2.transform.localScale.y < 2f)
                    {
                        Page1_gameObject_2.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 3
                    Page1_gameObject_3.SetActive(true);
                    if (Page1_gameObject_3.transform.localScale.y > 0.1f && Page1_gameObject_3.transform.localScale.y < 2f)
                    {
                        Page1_gameObject_3.gameObject.transform.localScale -= scaleChange;

                    }
                    //GameObject 3
                    Page1_gameObject_4.SetActive(true);
                    if (Page1_gameObject_4.transform.localScale.y > 0.1f && Page1_gameObject_4.transform.localScale.y < 2f)
                    {
                        Page1_gameObject_4.gameObject.transform.localScale -= scaleChange;

                    }

                    //Page 2 GameObjects Scalings To ( 2 to 0 )

                    //GameObject 1
                    Page2_gameObject_1.SetActive(true);
                    if (Page2_gameObject_1.transform.localScale.y > 0.1f && Page2_gameObject_1.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_1.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 2
                    Page2_gameObject_2.SetActive(true);
                    if (Page2_gameObject_2.transform.localScale.y > 0.1f && Page2_gameObject_2.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_2.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 3
                    Page2_gameObject_3.SetActive(true);
                    if (Page2_gameObject_3.transform.localScale.y > 0.1f && Page2_gameObject_3.transform.localScale.y < 2.1f)
                    {
                        Page2_gameObject_3.gameObject.transform.localScale -= scaleChange;

                    }
                //GameObject 3
                Page2_gameObject_4.SetActive(true);
                if (Page2_gameObject_4.transform.localScale.y > 0.1f && Page2_gameObject_4.transform.localScale.y < 2.1f)
                {
                    Page2_gameObject_4.gameObject.transform.localScale -= scaleChange;

                }

                //Page 3 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page3.SetActive(true);
                    if (Page3.transform.localScale.y > 0.1f && Page3.transform.localScale.y < 2.1f)
                    {
                        Page3.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 2
                    Page3_2.SetActive(true);
                    if (Page3_2.transform.localScale.y > 0.1f && Page3_2.transform.localScale.y < 2.1f)
                    {
                        Page3_2.gameObject.transform.localScale -= scaleChange;

                    }

                    //GameObject 3
                    Page3_3.SetActive(true);
                    if (Page3_3.transform.localScale.y > 0.1f && Page3_3.transform.localScale.y < 2.1f)
                    {
                        Page3_3.gameObject.transform.localScale -= scaleChange;

                    }

                }


                break;

        }

           
    }
}



