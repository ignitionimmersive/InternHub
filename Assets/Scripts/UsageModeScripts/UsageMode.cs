using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class UsageMode : MonoBehaviour
{
    public List<VideoClip> theVideos;
    public List<AudioClip> theAudios;


    public GameObject theVideoPlane;


    private void Start()
    {
        theVideoPlane.gameObject.AddComponent<AudioSource>();
        theVideoPlane.gameObject.AddComponent<VideoPlayer>();
        theVideoPlane.GetComponent<AudioSource>().playOnAwake = false;
        theVideoPlane.GetComponent<VideoPlayer>().playOnAwake = true;
        theVideoPlane.GetComponent<VideoPlayer>().isLooping = false;
        theVideoPlane.GetComponent<AudioSource>().loop = false;
        AssignVideo(0);
        

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position, this.gameObject.transform.position); 

        if (distance <= 2f)
        {
            //theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1 - distance);
            theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1f - (distance * .5f));
            //theVideoPlane.GetComponent<AudioSource>().volume = 1f - distance;
            theVideoPlane.GetComponent<AudioSource>().volume = 1f - (distance * .5f);
        }

        else
        {
            theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            theVideoPlane.GetComponent<AudioSource>().volume = 0;
        }
    }

    public void AssignVideo(int videoNumber)
    { 
        theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideos[videoNumber];
        theVideoPlane.GetComponent<AudioSource>().clip = this.theAudios[videoNumber];
        theVideoPlane.GetComponent<VideoPlayer>().Play();
        theVideoPlane.GetComponent<AudioSource>().Play();
    }

}
