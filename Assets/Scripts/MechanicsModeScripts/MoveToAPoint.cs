using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToAPoint : MonoBehaviour
{
    public enum MOVE_TO_A_POINT_STATE
    {
        INITIAL_POSITION,
        MOVE,
        FINAL_POSITION
    };

    public float moveSpeed;
    public float timeToStart;

    public GameObject target;
    [HideInInspector] public Vector3 finalPosition;
    [HideInInspector] public Quaternion finalRotation;

    private float startTime, journeyLength, distanceCovered, distanceProgress;

    private MOVE_TO_A_POINT_STATE currentState;

    public MOVE_TO_A_POINT_STATE CURRENTSTATE;

    void Start()
    {
        if (this.target != null)
        {
            finalPosition = target.transform.position;
            finalRotation = target.transform.rotation;
        }
        startTime = Time.time;
        journeyLength = Vector3.Distance(this.transform.position, finalPosition);
    }

    private void Update()
    {
        switch (CURRENTSTATE)
        {
            case MOVE_TO_A_POINT_STATE.INITIAL_POSITION:
                {
                    break;
                }
            case MOVE_TO_A_POINT_STATE.MOVE:
                {
              
                    if (moveSpeed == 0) moveSpeed = Random.Range(0.001f, 0.01f);
                    if (timeToStart == 0) timeToStart = Random.Range(0, 2f);

                    StartCoroutine(MoveToPoint(moveSpeed, timeToStart));

                    if ((this.gameObject.transform.position.x <= (this.finalPosition.x + 0.05)) &&
                        (this.gameObject.transform.position.x >= (this.finalPosition.x - 0.05))&&
                        (this.gameObject.transform.position.y <= (this.finalPosition.y + 0.05)) &&
                        (this.gameObject.transform.position.y >= (this.finalPosition.y - 0.05)) &&
                        (this.gameObject.transform.position.z <= (this.finalPosition.z + 0.05)) &&
                        (this.gameObject.transform.position.z >= (this.finalPosition.z - 0.05)))
                    {
                        this.gameObject.transform.position = this.finalPosition;
                        this.gameObject.GetComponent<MoveToAPoint>().CURRENTSTATE = MOVE_TO_A_POINT_STATE.FINAL_POSITION;
                    }

                    break;
                }
            case MOVE_TO_A_POINT_STATE.FINAL_POSITION:
                {
                    break;

                }
        }
    }

    IEnumerator MoveToPoint(float _speed,float _time )
    {
        yield return new WaitForSeconds(_time);
        distanceCovered = (Time.time - startTime) * _speed;
        distanceProgress = distanceCovered / journeyLength;

        this.transform.position = Vector3.Lerp(this.transform.position, finalPosition, distanceProgress);
        this.transform.rotation = Quaternion.Slerp(this.transform.rotation, finalRotation, distanceProgress);
    }
}
