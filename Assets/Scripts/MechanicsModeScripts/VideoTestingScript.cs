using UnityEngine;
using UnityEngine.Video;

public class VideoTestingScript : MonoBehaviour
{
    public GameObject videoPlayer;
    public GameObject theLens;

    private void Start()
    {
        videoPlayer.GetComponent<Renderer>().enabled = false;
        videoPlayer.GetComponent<VideoPlayer>().playOnAwake = true;
        videoPlayer.GetComponent<VideoPlayer>().isLooping = true;
        videoPlayer.GetComponent<VideoPlayer>().Pause();

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            videoPlayer.GetComponent<VideoPlayer>().Play();
            
            Debug.Log("Play Video");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            videoPlayer.GetComponent<VideoPlayer>().Pause();
           

            Debug.Log("Pause Video");
        }
    }
}


