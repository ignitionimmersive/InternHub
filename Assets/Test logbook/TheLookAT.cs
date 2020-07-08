using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLookAT : MonoBehaviour
{
    GameObject target;
    private void Start()
    {
        target = Camera.main.gameObject;
    }


    private void Update()
    {

        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);
    }

    void FixedUpdate()
    {
        //reading the input:
        float horizontalAxis = Input.GetAxis("Horizontal");
        float verticalAxis = Input.GetAxis("Vertical");


        //camera forward and right vectors:
        var forward = target.transform.forward;
        var right = target.transform.right;

        //project forward and right vectors on the horizontal plane (y = 0)
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();

        //this is the direction in the world space we want to move:
        var desiredMoveDirection = forward * verticalAxis + right * horizontalAxis;

        //now we can apply the movement:
        transform.Translate(desiredMoveDirection * 2 * Time.deltaTime);

    }
}
