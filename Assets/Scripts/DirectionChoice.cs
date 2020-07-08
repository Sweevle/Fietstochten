using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class DirectionChoice : MonoBehaviour
{
    private bool measuring = false;
    private bool directionSet = false;
    private string direction = "";
    private VideoPlayer videoPlayer;
    private string triggered = "Not in contact";
    GameObject directionArrow;  
    IEnumerator mL;
    Vector3 moveDirection = Vector3.back;
    Vector3 platePos;
    string vName;

    int videoID = 0;
    public GameObject directionChoiceTile;
    public worldScript world;
    public fadePanel fadeUI;
    public VideoManager videoManager;
    public Sprite leftArrow;
    public Sprite rightArrow;
    public Sprite defaultArrow;
    public Material skybox;

    GameObject cam;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        cam = GameObject.Find("MainCamera");
        mL = measureLean();
        // start voice explanation
    }

    // Update is called once per frame
    void Update()
    {

        if (directionSet == true){
            // fadeUI.Fade();
            world.fadedOut = true;
            world.elapsedTime = 0;
            //             videoPlayer.url = "file://mnt/sdcard/Videos/4.mp4";
            if (direction == "Left"){
                Debug.Log("direction is left and video is: " + vName);
                switch (vName){
                    case "1":
                        videoPlayer.url = "file://mnt/sdcard/Videos/4.mp4";
                        world.next_spawn_time = 52f;
                        break;
                    case "2":
                        videoPlayer.url = "file://mnt/sdcard/Videos/5.mp4";
                        world.next_spawn_time = 38f;
                        break;
                    case "4":
                        videoPlayer.url = "file://mnt/sdcard/Videos/13.mp4";
                        world.next_spawn_time = 111f;
                        break;
                    case "5":
                        videoPlayer.url = "file://mnt/sdcard/Videos/8.mp4";
                        world.next_spawn_time = 134f;
                        break;
                    case "6":
                        videoPlayer.url = "file://mnt/sdcard/Videos/9.mp4";
                        world.next_spawn_time = 33f;
                        break;
                    case "8":
                        videoPlayer.url = "file://mnt/sdcard/Videos/2.mp4";
                        world.next_spawn_time = 90f;
                        break;
                    case "9":
                        videoPlayer.url = "file://mnt/sdcard/Videos/6.mp4";
                        world.next_spawn_time = 5f;
                        break;
                    default:
                        break;
                }
                // fadeUI.Fade();
            }
            else if (direction == "Right"){
                Debug.Log("direction is right and video is: " + vName);
                //play video right

                switch (vName){
                    case "1":
                        videoPlayer.url = "file://mnt/sdcard/Videos/2.mp4";
                        world.next_spawn_time = 93f;
                        break;
                    case "3":
                        videoPlayer.url = "file://mnt/sdcard/Videos/9.mp4";
                        world.next_spawn_time = 33f;
                        break;
                    case "4":
                        videoPlayer.url = "file://mnt/sdcard/Videos/12.mp4";
                        world.next_spawn_time = 6f;
                        break;
                    case "5":
                        videoPlayer.url = "file://mnt/sdcard/Videos/7.mp4";
                        world.next_spawn_time = 44f;
                        break;
                    case "7":
                        videoPlayer.url = "file://mnt/sdcard/Videos/5.mp4";
                        world.next_spawn_time = 38f;
                        break;
                    case "8":
                        videoPlayer.url = "file://mnt/sdcard/Videos/4.mp4";
                        world.next_spawn_time = 52f;
                        break;
                    case "9":
                        videoPlayer.url = "file://mnt/sdcard/Videos/10.mp4";
                        world.next_spawn_time = 79f;
                        break;
                    case "10":
                        videoPlayer.url = "file://mnt/sdcard/Videos/3.mp4";
                        world.next_spawn_time = 111f;
                        break;
                    default:
                        break;
                }
                // fadeUI.Fade();
            }
            else{
                Debug.Log("direction is straight and video is: " + vName);
                //play video straight
                switch (vName){
                    case "1":
                        videoPlayer.url = "file://mnt/sdcard/Videos/3.mp4";
                        world.next_spawn_time = 111f;
                        break;
                    case "2":
                        videoPlayer.url = "file://mnt/sdcard/Videos/6.mp4";
                        world.next_spawn_time = 5f;
                        break;
                    case "3":
                        videoPlayer.url = "file://mnt/sdcard/Videos/7.mp4";
                        world.next_spawn_time = 44f;
                        break;
                    case "6":
                        videoPlayer.url = "file://mnt/sdcard/Videos/8.mp4";
                        world.next_spawn_time = 134f;
                        break;
                    case "7":
                        videoPlayer.url = "file://mnt/sdcard/Videos/10.mp4";
                        world.next_spawn_time = 79f;
                        break;
                    case "10":
                        videoPlayer.url = "file://mnt/sdcard/Videos/4.mp4";
                        world.next_spawn_time = 52f;
                        break;
                    default:
                        break;
                }
                // fadeUI.Fade();
            };
           directionSet = false;
           world.fadedOut = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(triggered = "In contact");
        world.shouldMove = false;
        directionArrow = GameObject.Find("Arrow");
        updateVideoName();
        StartCoroutine(mL);
    }

    private void OnTriggerExit(Collider other)
    {
        
        Debug.Log(triggered = "No longer in contact and direction is " + direction);
    
        StopCoroutine(mL);
        directionChoiceTile = GameObject.Find("directionChoiceTile");
        Destroy(directionChoiceTile);
        world.shouldMove = true;
        directionSet = true;
    }

    IEnumerator measureLean(){
        var f1 = 0.2f;
        var f2 = -0.2f;

        var controller = VZPlayer.controller;
        
        measuring = true; 
        float c = cam.transform.eulerAngles.y;

        while ( measuring ){
            switch (vName){
                case "1":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "2":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "3":
                    if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "4":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        if(Random.value<0.5f)
                            direction = "Right";
                        else
                            direction = "Left";
                    }
                    break;
                case "5":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        if(Random.value<0.5f)
                            direction = "Right";
                        else
                            direction = "Left";
                    }
                    break;
                case "6":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "7":
                    if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "8":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "9":
                    if (controller.HeadRot){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        if(Random.value<0.5f)
                            direction = "Right";
                        else
                            direction = "Left";
                    }
                    break;
                case "10":
                    if (controller.HeadRot){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                default:
                    break;
            }
          yield return null;
        }
        measuring = false;
    }

    public void updateVideoName(){
        vName = videoPlayer.url;
        vName = vName.Remove(0,25);
        // vName = vName.Remove(0,33);
        vName = vName.Replace(".mp4", "");
        Debug.Log("videoname is: " + vName);
    }

    
}
