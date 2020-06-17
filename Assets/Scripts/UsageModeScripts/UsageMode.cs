using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class UsageMode : MonoBehaviour
{
    public List<VideoClip> theVideos;
    public VideoClip theVideo;


    public GameObject theVideoPlane;


    private void Start()
    {
        theVideoPlane.gameObject.AddComponent<VideoPlayer>();
        //theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideo;
        //theVideoPlane.GetComponent<VideoPlayer>().Pause();
        theVideoPlane.GetComponent<VideoPlayer>().playOnAwake = true;
        theVideoPlane.GetComponent<VideoPlayer>().isLooping = false;
        AssignVideo(0);

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position, this.gameObject.transform.position); 

        if (distance <= 1)
        {
            theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1 - distance);
        }

        else
        {
            theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
        }
    }

    public void AssignVideo(int videoNumber)
    { 
        theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideos[videoNumber];
        
        theVideoPlane.GetComponent<VideoPlayer>().Play();
    }

}
