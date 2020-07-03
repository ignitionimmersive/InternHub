using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;


public class UsageMode : MonoBehaviour
{
    public List<VideoClip> theVideos;
    public List<AudioClip> theAudios;

    public GameObject theVideoPlane;

    VideoPlayer theVideoPlayer;
    AudioSource theAudioSource;

    private void Start()
    {
        theVideoPlane.gameObject.AddComponent<AudioSource>();
        theVideoPlane.gameObject.AddComponent<VideoPlayer>();

        theVideoPlayer = theVideoPlane.GetComponent<VideoPlayer>();
        theAudioSource = theVideoPlane.GetComponent<AudioSource>();

        theVideoPlayer.SetDirectAudioMute(0, true);
        theAudioSource.playOnAwake = false;
        theVideoPlayer.playOnAwake = true;
        theVideoPlayer.isLooping = false;
        theAudioSource.loop = false;
        AssignVideo(0);
        

    }

    private void Update()
    {
        float distance = Vector3.Distance(GameObject.FindWithTag("MainCamera").transform.position, this.gameObject.transform.position); 

        if (distance <= 2f)
        {
            //theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 1 - distance);
            //theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            //theVideoPlane.GetComponent<AudioSource>().volume = 1f - distance;
            theAudioSource.volume = 1f - (distance * .5f);
        }

        else
        {
            // theVideoPlane.GetComponent<VideoPlayer>().SetDirectAudioVolume(0, 0);
            theAudioSource.volume = 0;
        }
    }

    public void AssignVideo(int videoNumber)
    { 
        theVideoPlayer.clip = this.theVideos[videoNumber];
        theAudioSource.clip = this.theAudios[videoNumber];
        theAudioSource.Play();
        theAudioSource.Play();
    }

}
