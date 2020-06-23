using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] GameObject flip;
    [SerializeField] GameObject revrseFlip;
    
    public GameObject Page1;
    public GameObject Page2;
    
    private Vector3 scaleChange;
   
    public enum STATES
    {
        CLOSE,
        OPEN,
        PAGE,PAGE_REVERSE,
        PAGE1, PAGE1_REVERSE,
        PAGE2, PAGE2_REVERSE,
        PAGE3, PAGE3_REVERSE
    };

    public STATES CURRENTSTATE;

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        
        
        anim.GetComponent<Animator>();
        scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
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
                    
                        switch (CURRENTSTATE)
                        {

                        case STATES.CLOSE:
                            {
                                this.CURRENTSTATE = STATES.OPEN;
                                break;
                            }
                        case STATES.OPEN:

                                {
                                    if (hit.collider.gameObject.name == "Flip")
                                    {
                                        
                                        anim.Play("Open");
                                        

                                        

                                        this.CURRENTSTATE = STATES.PAGE;
                                    }
                                   

                                    break;
                                }
                        
                            
                        case STATES.PAGE:
                                {
                                
                                    if ((hit.collider.gameObject.name == "Flip"))
                                    {
                                    
                                    anim.Play("Page Flip");
                                    
                                    this.CURRENTSTATE = STATES.PAGE1;
                                   
                                    }
                                

                                if (hit.collider.gameObject.name == "ReverseFlip")
                                    {
                                        anim.Play("Close");
                                        this.CURRENTSTATE = STATES.OPEN;
                                    }

                                break;
                                 }
                            
                        
                        case STATES.PAGE1:
                            {
                                if (hit.collider.gameObject.name == "ReverseFlip")
                                {
                                    
                                    anim.Play("Page Flip Reverse");
                                    this.CURRENTSTATE = STATES.PAGE;

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
            while(true)
            {
                if(CURRENTSTATE == STATES.PAGE)
                {
                Page1.SetActive(true);
                if (Page1.transform.localScale.y < 2f && Page1.transform.localScale.y > -0.1f)
                    {
                        Page1.gameObject.transform.localScale += scaleChange;
                    }
                Page2.SetActive(true);
                if (Page2.transform.localScale.y > 0f && Page2.transform.localScale.y < 3f)
                {
                    Page2.gameObject.transform.localScale -= scaleChange;

                }
            }
            if (CURRENTSTATE == STATES.PAGE_REVERSE)
            {
                
                    Page1.SetActive(true);
                    if (Page1.transform.localScale.y < 2f && Page1.transform.localScale.y > -0.1f)
                    {
                        Page1.gameObject.transform.localScale += scaleChange;
                    }
                   
                    
                
            }

            if (CURRENTSTATE == STATES.PAGE1)
                {
                Page1.SetActive(true);
                if (Page1.transform.localScale.y > 0f && Page1.transform.localScale.y < 3f)
                    {
                        Page1.gameObject.transform.localScale -= scaleChange;
                        
                    }
                Page2.SetActive(true);
                if (Page2.transform.localScale.y < 2f && Page2.transform.localScale.y > -0.1f)
                {
                    Page2.gameObject.transform.localScale += scaleChange;
                }

            }
            if (CURRENTSTATE == STATES.OPEN)
            {
                Page1.SetActive(true);
                if (Page1.transform.localScale.y > 0f && Page1.transform.localScale.y < 3f)
                {
                    Page1.gameObject.transform.localScale -= scaleChange;

                }
                Page2.SetActive(true);
                if (Page2.transform.localScale.y > 0f && Page2.transform.localScale.y < 3f)
                {
                    Page2.gameObject.transform.localScale -= scaleChange;

                }
            }

            break;
            }
      
        
        

    }
}

