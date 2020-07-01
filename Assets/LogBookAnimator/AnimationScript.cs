using System.Collections;
using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;


public class AnimationScript : MonoBehaviour
{


    public GameObject Flip;
    public GameObject ReverseFlip;
    public AudioSource Audio;
    public AudioSource OpenClose;
    public AudioSource ReverseAudio;




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

    public GameObject page4_1;
    public GameObject page4_2;
    
   

    private Vector3 scaleChange;

    public enum STATES
    {

        OPEN,
        PAGE1,
        PAGE2,
        PAGE3,
        PAGE4
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

                                    PopUpText.SetActive(false);
                                    OpenClose.Play();
                                    anim.Play("Open");
                                    this.CURRENTSTATE = STATES.PAGE1;
                                }
                                break;
                            }


                        case STATES.PAGE1:
                            {
                                if ((hit.collider.gameObject.name == "Flip"))
                                {
                                    PlayAnim("Page Flip", STATES.PAGE2);
                                }

                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {
                                    OpenClose.Play();
                                    anim.Play("Close");
                                    this.CURRENTSTATE = STATES.OPEN;
                                }

                                break;
                            }


                        case STATES.PAGE2:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {
                                    PlayAnim("Page Flip Reverse", STATES.PAGE1);
                                }

                                if (hit.collider.gameObject.name == "Flip")
                                {
                                    PlayAnim("Page Flip 0", STATES.PAGE3);
                                }

                                break;
                            }

                        case STATES.PAGE3:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {

                                    PlayAnim("Page Flip Reverse 0", STATES.PAGE2);

                                }

                                if (hit.collider.gameObject.name == "Flip")
                                {
                                    PlayAnim("Page Flip 1", STATES.PAGE4);

                                }
                                break;
                            }
                        case STATES.PAGE4:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {

                                    PlayAnim("Page Flip Reverse 1", STATES.PAGE3);

                                }

                                if (hit.collider.gameObject.name == "Flip")
                                {
                                    OpenClose.Play();
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
                ScaleUp(Page1_gameObject_1);

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                ScaleUp(Page1_gameObject_2);

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                ScaleUp(Page1_gameObject_3);

                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                ScaleUp(Page1_gameObject_4);

                //Page 2 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                ScaleDown(Page2_gameObject_1);

                //GameObject 2
                Page2_gameObject_2.SetActive(true);
                ScaleDown(Page2_gameObject_2);

                //GameObject 3
                Page2_gameObject_3.SetActive(true);
                ScaleDown(Page2_gameObject_3);

                //GameObject 4
                Page2_gameObject_4.SetActive(true);
                ScaleDown(Page2_gameObject_4);

            }

            else if (CURRENTSTATE == STATES.PAGE2)
            {
                // Page 1 GameObjects Scaling Back To 0 If The State Changes to Page2

                //GameObject 1
                Page1_gameObject_1.SetActive(true);
                ScaleDown(Page1_gameObject_1);

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                ScaleDown(Page1_gameObject_2);

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                ScaleDown(Page1_gameObject_3);

                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                ScaleDown(Page1_gameObject_4);

                // Page 2 GameObjects Scaling ( 0 to 2 )

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                ScaleUp(Page2_gameObject_1);

                //GameObject 2
                Page2_gameObject_2.SetActive(true);
                ScaleUp(Page2_gameObject_2);

                //GameObject 3
                Page2_gameObject_3.SetActive(true);
                ScaleUp(Page2_gameObject_3);
                //GameObject 3
                Page2_gameObject_4.SetActive(true);
                ScaleUp(Page2_gameObject_4);

                //Page 3 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page3.SetActive(true);
                ScaleDown(Page3);

                

            }


            else if (CURRENTSTATE == STATES.PAGE3)
            {


                //Page 2 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                ScaleDown(Page2_gameObject_1);

                //GameObject 2
                Page2_gameObject_2.SetActive(true);
                ScaleDown(Page2_gameObject_2);

                //GameObject 3
                Page2_gameObject_3.SetActive(true);
                ScaleDown(Page2_gameObject_3);

                //GameObject 4
                Page2_gameObject_4.SetActive(true);
                ScaleDown(Page2_gameObject_4);

                //Page 3 GameObjects Scaling ( 0 to 2 )

                //GameObject 1
                Page3.SetActive(true);
                ScaleUp(Page3);

                //page 4 GameObjects
                page4_1.SetActive(true);
                ScaleDown(page4_1);

                page4_2.SetActive(true);
                ScaleDown(page4_2);

            }
            
            else if(CURRENTSTATE == STATES.PAGE4)
            {
                // page 3 GameObjects Scale ( 1 to 0 )

                //GameObject 1
                Page3.SetActive(true);
                ScaleDown(Page3);


                page4_1.SetActive(true);
                ScaleUp(page4_1);

                page4_2.SetActive(true);
                ScaleUp(page4_2);
            }


            else if (CURRENTSTATE == STATES.OPEN)
            {
                PopUpText.SetActive(true);
                //Reverting Back to Scale 0 if the Book is Closed.

                //Page 1 Gameobjects

                //GameObject 1
                Page1_gameObject_1.SetActive(true);
                ScaleDown(Page1_gameObject_1);

                //GameObject 2
                Page1_gameObject_2.SetActive(true);
                ScaleDown(Page1_gameObject_2);

                //GameObject 3
                Page1_gameObject_3.SetActive(true);
                ScaleDown(Page1_gameObject_3);

                //GameObject 4
                Page1_gameObject_4.SetActive(true);
                ScaleDown(Page1_gameObject_4);

                //Page 2 GameObjects 

                //GameObject 1
                Page2_gameObject_1.SetActive(true);
                ScaleDown(Page2_gameObject_1);

                //GameObject 2
                Page2_gameObject_2.SetActive(true);
                ScaleDown(Page2_gameObject_2);

                //GameObject 3
                Page2_gameObject_3.SetActive(true);
                ScaleDown(Page2_gameObject_3);

                //GameObject 4
                Page2_gameObject_4.SetActive(true);
                ScaleDown(Page2_gameObject_4);

                //Page 3 GameObjects Scalings To ( 2 to 0 )

                //GameObject 1
                Page3.SetActive(true);
                ScaleDown(Page3);

                //page 4 GameObjects
                page4_1.SetActive(true);
                ScaleDown(page4_1);

                page4_2.SetActive(true);
                ScaleDown(page4_2);
            }
            break;
        }
    }

    void ScaleUp(GameObject obj)
    {
        if (obj.transform.localScale.y < 1f && obj.transform.localScale.y > -0.1f)
        {
            obj.transform.localScale += scaleChange;
        }
    }

    void ScaleDown(GameObject obj)
    {
        if (obj.transform.localScale.y > 0.1f && obj.transform.localScale.y < 2.1f)
        {
            obj.transform.localScale -= scaleChange;

        }
    }

    void PlayAnim(string animName, STATES state)
    {
        Audio.Play();
        anim.Play(animName);
        this.CURRENTSTATE = state;
    }

  
    void StopAudioNow()
    {
        ReverseAudio.Stop();
    }
}



