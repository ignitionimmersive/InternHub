using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject spitFire;

    private Touch touch;
    [Range(0.00001f, 1f)] public float speedModifier = 1f;

    private Vector3 initialPosition;
    private void Start()
    {

        initialPosition = this.gameObject.transform.position;
    }
    private void Update()
    {
        
            if (Input.touchCount > 0)
            {
            touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Moved)
                {
                
                    transform.position = new Vector3(
                        transform.position.x + touch.deltaPosition.x * speedModifier,
                        transform.position.y,
                        transform.position.z + touch.deltaPosition.y * speedModifier);
                }
            }

           

    }
    public void resetToInitialPosition()
    {
        this.gameObject.transform.position = initialPosition;
        this.gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.name == "Spitfire")
        {
            other.gameObject.GetComponent<Animator>().enabled = true;
            other.gameObject.GetComponent<Animator>().SetInteger("SpitfireAnimController", 2);

            resetToInitialPosition();
            

        }
    }

}
