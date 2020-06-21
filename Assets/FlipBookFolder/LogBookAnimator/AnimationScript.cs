using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    [SerializeField] GameObject flip;
    [SerializeField] GameObject revrseFlip;
    public enum STATES
    {
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
                    
                        switch (CURRENTSTATE)
                        {
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
                                    this.CURRENTSTATE = STATES.PAGE_REVERSE;
                                   
                                }
                                
                                    else if (hit.collider.gameObject.name == "ReverseFlip")
                                    {
                                        anim.Play("Close");
                                        this.CURRENTSTATE = STATES.OPEN;
                                    }
                                break;
                                 }
                            
                        case STATES.PAGE_REVERSE:
                                {
                                    if (hit.collider.gameObject.name == "ReverseFlip")
                                    {
                                    this.CURRENTSTATE = STATES.PAGE;
                                    anim.Play("Page Flip Reverse");
                                    
                                    
                                    }
                                    break;
                                }
                        
                           


                        }
                       
                    }



                }

            }
        
    }
}

