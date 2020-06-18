using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move_to_point : MonoBehaviour
{

    public Transform otherCube;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.AddComponent<MoveToAPoint>();
        this.GetComponent<MoveToAPoint>().finalPosition = otherCube.position;
        this.GetComponent<MoveToAPoint>().moveSpeed = 0.001f;
        this.GetComponent<MoveToAPoint>().CURRENTSTATE = MoveToAPoint.MOVE_TO_A_POINT_STATE.MOVE;
        

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<MoveToAPoint>().CURRENTSTATE == MoveToAPoint.MOVE_TO_A_POINT_STATE.FINAL_POSITION)
        {
            Destroy(this.GetComponent<MoveToAPoint>());
        }
    }
}
