using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public Animator birdAnim;
    public bool fly;
    public bool idle;
    //------------//

    #region Private Functions
    
    void Start()
    {
        if (fly)
        {
            birdAnim.SetBool("Fly", true);
            return;
        }
        else if (idle)
        {
            birdAnim.SetBool("Idle", true);
            return;
        }
    }

    void Update()
    {
        
    }

    #endregion

    //------------//

    #region Public Functions
    
    #endregion
}
