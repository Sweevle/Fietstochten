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
    
    int videoID = 0;
    public GameObject choiceTile;
    public GameObject directionChoiceTile;
    public worldScript world;
    public fadePanel fadeUI;
    public VideoManager videoManager;
    public Sprite leftArrow;
    public Sprite rightArrow;
    public Sprite defaultArrow;
    public Material skybox;

    // Start is called before the first frame update
    void Start()
    {
        skybox.SetInt( "_Rotation" ,  293);
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        mL = measureLean();
        // start voice explanation
        // audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {

        if (directionSet == true){
            // fadeUI.Fade();
            world.fadedOut = true;
            world.elapsedTime = 0;
            if (direction == "Left"){
                videoPlayer.url = "Assets/Videos/ChoiceLeft.mp4";
                skybox.SetInt( "_Rotation" ,  276);
                // fadeUI.Fade();
                // videoPlayer.url = "Assets/Videos/ChoiceLRight.mp4";
                // skybox.SetInt( "_Rotation" ,  281);
            }
            else if (direction == "Right"){
                //play video right
                videoPlayer.url = "Assets/Videos/ChoiceRight.mp4";
                skybox.SetInt( "_Rotation" ,  280);
                // fadeUI.Fade();
            }
            else{
                //play video straight
                videoPlayer.url = "Assets/Videos/ChoiceStraight.mp4";
                skybox.SetInt( "_Rotation" ,  265);
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
        measuring = true; 
        var controller = VZPlayer.Controller;
        var f1 = 0.2;
        var f2 = -0.2;

        while ( measuring ) {
            if (controller.HeadRot > f1){
                direction = "Left";
                directionArrow.GetComponent<SpriteRenderer>().sprite = leftArrow;
            } else if ( controller.HeadRot < f2){
                direction = "Right";
                directionArrow.GetComponent<SpriteRenderer>().sprite = rightArrow;
            } else {
                direction = "Straight";
                directionArrow.GetComponent<SpriteRenderer>().sprite = defaultArrow;
            }
          yield return null;
        }
        measuring = false;
    }

    
}
