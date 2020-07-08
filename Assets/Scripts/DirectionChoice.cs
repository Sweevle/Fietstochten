using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    GameObject cam;
    string vName;

    public GameObject directionChoiceTile;
    public worldScript world;
    public fadePanel fadeUI;
    public VideoManager videoManager;
    public Sprite leftArrow;
    public Sprite rightArrow;
    public Sprite defaultArrow;

    // Start is called before the first frame update
    void Start()
    {
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        cam = GameObject.Find("MainCamera");
        mL = measureLean();
    }

    // Update is called once per frame
    void Update()
    {

        if (directionSet == true){
            // fadeUI.Fade();
            world.fadedOut = true;
            world.elapsedTime = 0;


            // based on the current video and the chosen direction set the new video and next spawn time of direction choice
            if (direction == "Left"){
                Debug.Log("direction is left and video is: " + vName);
                switch (vName){
                    case "1":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "4.mp4");
                        world.next_spawn_time = 52f;
                        break;
                    case "2":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "5.mp4");
                        world.next_spawn_time = 38f;
                        break;
                    case "4":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "13.mp4");
                        world.next_spawn_time = 111f;
                        break;
                    case "5":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "8.mp4");
                        world.next_spawn_time = 134f;
                        break;
                    case "6":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "9.mp4");
                        world.next_spawn_time = 33f;
                        break;
                    case "8":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "2.mp4");
                        world.next_spawn_time = 90f;
                        break;
                    case "9":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "6.mp4");
                        world.next_spawn_time = 5f;
                        break;
                    default:
                        break;
                }
                // fadeUI.Fade();
            }
            else if (direction == "Right"){
                Debug.Log("direction is right and video is: " + vName);
                switch (vName){
                    case "1":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "2.mp4");
                        world.next_spawn_time = 93f;
                        break;
                    case "3":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "9.mp4");
                        world.next_spawn_time = 33f;
                        break;
                    case "4":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "12.mp4");
                        world.next_spawn_time = 6f;
                        break;
                    case "5":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "7.mp4");
                        world.next_spawn_time = 44f;
                        break;
                    case "7":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "5.mp4");
                        world.next_spawn_time = 38f;
                        break;
                    case "8":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "4.mp4");
                        world.next_spawn_time = 52f;
                        break;
                    case "9":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "10.mp4");
                        world.next_spawn_time = 79f;
                        break;
                    case "10":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "3.mp4");
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
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "3.mp4");
                        world.next_spawn_time = 111f;
                        break;
                    case "2":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "6.mp4");
                        world.next_spawn_time = 5f;
                        break;
                    case "3":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "7.mp4");
                        world.next_spawn_time = 44f;
                        break;
                    case "6":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "8.mp4");
                        world.next_spawn_time = 134f;
                        break;
                    case "7":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "10.mp4");
                        world.next_spawn_time = 79f;
                        break;
                    case "10":
                        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "4.mp4");
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
        
        // only allow the plate under the arrow
        world.shouldMove = false;
        directionArrow = GameObject.Find("Arrow");

        // check video name and start measuring
        updateVideoName();
        StartCoroutine(mL);
    }

    private void OnTriggerExit(Collider other)
    {
        
        Debug.Log(triggered = "No longer in contact and direction is " + direction);
        
        // stop measuring
        StopCoroutine(mL);
        
        // remove the direction choice object
        directionChoiceTile = GameObject.Find("directionChoiceTile");
        Destroy(directionChoiceTile);

        // set the bools back to moving
        world.shouldMove = true;

        // direction has been chosen
        directionSet = true;
    }

    IEnumerator measureLean(){
        measuring = true; 
        var f1 = 0.2f;
        var f2 = -0.2f;

        while ( measuring ){
            var controller = VZPlayer.Controller;

            //Based on the name of the video it will show the directions it can go and change the arrow accordingly.
            switch (vName){
                case "1":
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot < f2){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "2":
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "3":
                    if (controller.HeadRot < f2){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "4":
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot < f2){
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
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot < f2){
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
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "7":
                    if (controller.HeadRot < f2){
                        direction = "Right";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "8":
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else {
                        direction = "Straight";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
                    }
                    break;
                case "9":
                    if (controller.HeadRot > f1){
                        direction = "Left";
                        directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
                    } else if (controller.HeadRot < f2){
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
                    if (controller.HeadRot < f2){
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

        //remove the first part of the file path
        vName = vName.Replace(Application.streamingAssetsPath, "");

        //if there is a \ in the file path it will be removed -- used on pc
        vName = vName.Replace("\\", "");

        // if there is a / in the file path it will be removed -- used on oculus go
        vName = vName.Replace("/", "");
        vName = vName.Replace(".mp4", "");
        Debug.Log("videoname is: " + vName);
    }

    
}
