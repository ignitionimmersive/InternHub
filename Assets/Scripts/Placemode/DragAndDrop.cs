using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    private Touch touch;
    [Range(0.00001f, 0.02f)] public float speedModifier = 0.0004f;
    private void Start()
    {
        speedModifier = 0.00001f;
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided");
        if (other.gameObject.name == "Spitfire")
        {
            other.gameObject.GetComponent<Animator>().enabled = true;
            
            this.gameObject.SetActive(false);
        }
    }

}
