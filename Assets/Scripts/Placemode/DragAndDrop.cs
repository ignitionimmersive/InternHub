
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public GameObject spitFire;

    private Touch touch;
    [Range(0.00001f, 1f)] public float speedModifier = 0.1f;

    GameObject target ;

    private Vector3 initialPosition;
    private void Start()
    {
        target = Camera.main.gameObject;
        initialPosition = this.gameObject.transform.position;
    }
    private void Update()
    {
        Vector3 targetPosition = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
        transform.LookAt(targetPosition);

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
                if (touch.phase == TouchPhase.Moved)
                {
                
                    this.gameObject.transform.localPosition = new Vector3(
                        this.gameObject.transform.localPosition.x + touch.deltaPosition.x * speedModifier,
                        transform.localPosition.y,
                        transform.localPosition.z);
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
