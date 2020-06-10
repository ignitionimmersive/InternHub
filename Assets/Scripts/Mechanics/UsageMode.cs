using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;

public class UsageMode : MonoBehaviour
{
    
    public VideoClip theVideo;


    public GameObject theVideoPlane;

    private void Start()
    {
        
        theVideoPlane.gameObject.AddComponent<VideoPlayer>();
        theVideoPlane.gameObject.AddComponent<AudioSource>();
        theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideo;
        theVideoPlane.GetComponent<VideoPlayer>().Pause();
        theVideoPlane.GetComponent<VideoPlayer>().playOnAwake = false;
        theVideoPlane.GetComponent<VideoPlayer>().isLooping = true;

    }
    void OnCollisionEnter(Collision other)
    {
       // if (other.gameObject.tag == "Player")
        {
            theVideoPlane.GetComponent<VideoPlayer>().Play();
            Debug.Log("Collided");
        }
    }

    void OnCollisionExit(Collision other)
    {
       //if (other.gameObject.tag == "Player")
        {
            theVideoPlane.GetComponent<VideoPlayer>().Pause();
            Debug.Log("exit");
        }
    }
}
