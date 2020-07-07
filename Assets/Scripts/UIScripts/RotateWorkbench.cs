using UnityEngine;

public class RotateWorkbench : MonoBehaviour
{
    public GameObject WorkbenchParent;

    public Collider2D leftButton;
    public Collider2D rightButton;

    public float rotateSpeed = 100;


    private void Update()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
        Vector3 direction = worldTouchPosition - Camera.main.transform.position;
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
        {
            if (hit.collider == leftButton)
            {
                WorkbenchParent.transform.Rotate(0, 10* Time.deltaTime, 0);
            }
            else if (hit.collider == rightButton)
            {
                WorkbenchParent.transform.Rotate(0, -10 * Time.deltaTime, 0);
            }
        }
    }

    public void RotateLeft()
    {
        WorkbenchParent.transform.Rotate(0, -1 *  rotateSpeed * Time.deltaTime, 0);
    }
    public void RotateRight()
    {
        WorkbenchParent.transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }
}
