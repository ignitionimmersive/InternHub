using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateWorkbench : MonoBehaviour
{
    public GameObject WorkbenchParent;

    public float rotateSpeed = 100;


    public void RotateLeft()
    {
        WorkbenchParent.transform.Rotate(0, (-1) *  rotateSpeed * Time.deltaTime, 0);
    }
    public void RotateRight()
    {
        WorkbenchParent.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
