using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BuildModeScript : MonoBehaviour
{
    public List<GameObject> ScopeParts;
    public List<Transform> InScopeTransform;
    public List<Transform> InBlueprintTransform;


    public Transform blue;

    public bool OnScope;

    public int interpolationFrameCount = 1;
    int elapsedFrames = 0;
    
    private void Update()
    {
        Touch touch = Input.GetTouch(0);


        if ((Input.touchCount > 0) && (touch.phase == TouchPhase.Began))
        {
            
            if (OnScope == false)
            {
                this.OnScope = true;
            }

            else if (OnScope == true)
            {
                this.OnScope = false;
            }
        }

           if (!OnScope)
        {
            MoveToBlueprint();
        }
           else
        {
            MoveToScope();
        }

    }

    void MoveToBlueprint()
    { 
        foreach (GameObject scopePart in ScopeParts)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFrameCount;
            int thisIndex = ScopeParts.IndexOf(scopePart);
            scopePart.transform.position = Vector3.Lerp(scopePart.transform.position, InBlueprintTransform[thisIndex].position, interpolationRatio);
            scopePart.transform.rotation = Quaternion.Slerp(scopePart.transform.rotation, InBlueprintTransform[thisIndex].rotation, interpolationRatio);

            elapsedFrames = (elapsedFrames + 1) % (interpolationFrameCount + 1);
        }
    }

    void MoveToScope()
    {
        foreach (GameObject scopePart in ScopeParts)
        {
            float interpolationRatio = (float)elapsedFrames / interpolationFrameCount;
            int thisIndex = ScopeParts.IndexOf(scopePart);
            scopePart.transform.position = Vector3.Lerp(scopePart.transform.position, InScopeTransform[thisIndex].position, interpolationRatio);
            scopePart.transform.rotation = Quaternion.Slerp(scopePart.transform.rotation, InScopeTransform[thisIndex].rotation, interpolationRatio);

            elapsedFrames = (elapsedFrames + 1) % (interpolationFrameCount + 1);
        }
    }
}
