using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class UsageController : MonoBehaviour
{
    public Animator animator;

    public Collider mapHandle;

    [Header("Buttons")]
    public GameObject mapButtons;
    public GameObject button01;
    public GameObject button02;
    public GameObject button03;
    public GameObject button04;
    public GameObject button05;
    public GameObject button06;
    // public UsageMode UsageMode01;
    //public GameObject theLens;
    [Header("Video")]
    public GameObject theLens;
    public AudioSource theAudio;
    public VideoPlayer theVideo;

    public List<VideoClip> theVideos;
    public List<AudioClip> theAudios;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Ended))
        {
            Touch touch = Input.GetTouch(0);
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, 100f));
            Vector3 direction = worldTouchPosition - Camera.main.transform.position;
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.transform.position, direction, out hit))
            {
                if (hit.collider.gameObject == button01)
                {
                    theLens.SetActive(true);
                    AssignVideo(0);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                }

                else if (hit.collider.gameObject == button02)
                {
                    theLens.SetActive(true);
                    AssignVideo(1);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                    //workBench.GetComponent<Animator>().enabled = false;
                }
                else if (hit.collider.gameObject == button03)
                {
                    theLens.SetActive(true);
                    AssignVideo(2);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                }
                else if (hit.collider.gameObject == button04)
                {
                    theLens.SetActive(true);
                    AssignVideo(3);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                }
                else if (hit.collider.gameObject == button05)
                {
                    theLens.SetActive(true);
                    AssignVideo(4);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                }
                else if (hit.collider.gameObject == button06)
                {
                    theLens.SetActive(true);
                    AssignVideo(5);
                    animator.SetInteger("MapController", 2);
                    mapHandle.enabled = (true);
                    mapButtons.SetActive(false);
                }

                else if (hit.collider == mapHandle)
                {
                    animator.SetInteger("MapController", 1);
                    mapButtons.SetActive(true);
                }
            }
        }
    }


    public void AssignVideo(int videoNumber)
    {
        theVideo = theLens.GetComponent<VideoPlayer>();
        theAudio = theLens.GetComponent<AudioSource>();
        theVideo.clip = this.theVideos[videoNumber];
        theAudio.clip = this.theAudios[videoNumber];
        theVideo.Play();
        theAudio.Play();
    }

    public void StartMode()
    {
        animator.SetInteger("MapController", 1);
        mapButtons.SetActive(true);
    }

    public void ExitMode()
    {
        animator.SetInteger("MapController", 0);
        mapButtons.SetActive(false);
        theLens.SetActive(false);
    }
}
