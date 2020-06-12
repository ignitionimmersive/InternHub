using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Video;
using JetBrains.Annotations;

public class UsageMode : MonoBehaviour
{
    
    public VideoClip theVideo;


    public GameObject theVideoPlane;

    public float requiredDistance = 1f;

    private void Start()
    {
        
        theVideoPlane.gameObject.AddComponent<VideoPlayer>();
        theVideoPlane.gameObject.AddComponent<AudioSource>();
        theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideo;
        theVideoPlane.GetComponent<VideoPlayer>().Pause();
        theVideoPlane.GetComponent<VideoPlayer>().playOnAwake = false;
        theVideoPlane.GetComponent<VideoPlayer>().isLooping = true;

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position, this.gameObject.transform.position); 

        if (distance <= requiredDistance)
        {
            PlayTheVideo();
        }

        else
        {
        }
    }

    public void PlayTheVideo()
    {
        theVideoPlane.GetComponent<VideoPlayer>().Play();
    }

    public void PauseTheVideo()
    {

        theVideoPlane.GetComponent<VideoPlayer>().Pause();
    }

    /*
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
    */
}
