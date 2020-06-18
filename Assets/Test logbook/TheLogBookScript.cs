using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheLogBookScript : MonoBehaviour
{
    public Animator AnimatorController;
    public GameObject leftPage;
    public GameObject rightPage;

    private void Update()
    {
        //AnimatorController.SetInteger("Anim01", 0);
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == leftPage)
                {

                    AnimatorController.SetInteger("Anim01", 1);
                }

                else if (hit.collider.gameObject == rightPage)
                {
                    AnimatorController.SetInteger("Anim01", 0);
                }

            }

            

        }
    }

    private void LateUpdate()
    {
        //AnimatorController.SetInteger("Anim01", 5);
    }
}