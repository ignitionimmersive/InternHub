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
        /*
        LensVideoPlayer = new GameObject();
        LensVideoPlayer.name = "LensVideoPlayer";
        LensVideoPlayer.AddComponent<Canvas>();
        LensVideoPlayer.GetComponent<Canvas>().renderMode = RenderMode.WorldSpace;
        LensVideoPlayer.transform.SetParent(this.gameObject.transform);
        LensVideoPlayer.GetComponent<Canvas>().transform.position = this.gameObject.transform.position;
        LensVideoPlayer.AddComponent<VideoPlayer>().clip = theVideo;
        Lens
        /// stoop here

       


        var videoPlayer = theVideoPlane.gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
        var audioSource = theVideoPlane.gameObject.AddComponent<AudioSource>();

        videoPlayer.playOnAwake = true;
        videoPlayer.clip = theVideo;
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
        videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
        videoPlayer.targetMaterialProperty = "_MainTex";
        videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
        videoPlayer.SetTargetAudioSource(0, audioSource);
        this.theVideoPlane.gameObject.GetComponent<UnityEngine.Video.VideoPlayer>().Play();

         */

        theVideoPlane.gameObject.AddComponent<VideoPlayer>();
        theVideoPlane.gameObject.AddComponent<AudioSource>();
        theVideoPlane.GetComponent<VideoPlayer>().clip = this.theVideo;
        theVideoPlane.GetComponent<VideoPlayer>().Pause();
        theVideoPlane.GetComponent<VideoPlayer>().isLooping = true;

    }
    void OnTriggerEnter(Collider other)
    {
        theVideoPlane.GetComponent<VideoPlayer>().Play();
        Debug.Log("Collided");
    }

    void OnTriggerExit(Collider other)
    {
        theVideoPlane.GetComponent<VideoPlayer>().Stop();
        Debug.Log("exit");

    }
}
