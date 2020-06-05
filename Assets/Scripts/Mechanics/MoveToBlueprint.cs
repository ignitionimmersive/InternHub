using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToBlueprint : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2;
    public GameObject placeholder;
    private float startTime, journeyLength, distCovered, fracJourney;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector3.Distance(this.transform.position, placeholder.transform.position);
    }
    void Update()
    {
        MoveToPoint(moveSpeed);

        if ( this.gameObject.GetComponent<ChildBody>() != null)
        {
            if (this.gameObject.GetComponent<ChildBody>().transform.position == this.placeholder.transform.position)
            {
                this.gameObject.GetComponent<ChildBody>().CURRENTSTATE = ChildBody.STATES.ON_BLUEPRINT;
            }
        }
    }

    void MoveToPoint( float _speed)
    {
        distCovered = (Time.time - startTime) * _speed;
        fracJourney = distCovered / journeyLength;

        this.transform.position = Vector3.Lerp(this.transform.position, placeholder.transform.position, fracJourney);

    }
}
