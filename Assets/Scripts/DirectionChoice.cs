using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class DirectionChoice : MonoBehaviour
{
    private bool measuring = false;
    private bool shouldMove = true;
    private bool directionSet = false;
    private bool tutorialDone = false;
    private string direction = "";
    private AudioSource audioSource;
    private VideoPlayer videoPlayer;
    private float elapsedTime;
    private string triggered = "Not in contact";
    private GUIStyle guiStyle = new GUIStyle();
    GameObject mTile;
    GameObject mChoice;    
    IEnumerator mL;
    Vector3 moveDirection = Vector3.back;
    Vector3 platePos;
    GameObject canvas;
    int videoID = 0;
    public GameObject choiceTile;
    public Sprite leftArrow;
    public Sprite rightArrow;
    public Sprite defaultArrow;
    public Material skybox;

    
    
    

    // Start is called before the first frame update
    void Start()
    {
        skybox.SetInt( "_Rotation" ,  293);
        mTile = GameObject.Find("TilePrefab");
        mChoice = GameObject.Find("ChoiceTime");
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        canvas = GameObject.Find("CanvasUI");
        mL = measureLean();
        // start voice explanation
        audioSource.Play();
        // spawnTile();
    }

    // Update is called once per frame
    void Update()
    {
        var timeText = canvas.transform.Find("Time").GetComponent<Text>();
        elapsedTime += Time.deltaTime;
        timeText.text = elapsedTime.ToString("f0");
        
        if (!audioSource.isPlaying)
        {
            
            
            if (mTile && mChoice != null){
                if (shouldMove){
                    Debug.Log("not measuring");
                    mTile.transform.Translate(Vector3.back * Time.deltaTime * 6, Space.World);
                }
                mChoice.transform.Translate(Vector3.back * Time.deltaTime * 6, Space.World);
            }
        }

        if (directionSet){
            if (videoID == 0){
                if (direction == "Left"){
                    videoPlayer.url = "Assets/Videos/ChoiceLeft.mp4";
                    skybox.SetInt( "_Rotation" ,  276);

                    videoID = 1;

                    // videoPlayer.url = "Assets/Videos/ChoiceLRight.mp4";
                    // skybox.SetInt( "_Rotation" ,  281);
                }
                else if (direction == "Right"){
                    //play video right
                    videoPlayer.url = "Assets/Videos/ChoiceRight.mp4";
                    skybox.SetInt( "_Rotation" ,  280);

                    videoID = 2;
                }
                else{
                    //play video straight
                    videoPlayer.url = "Assets/Videos/ChoiceStraight.mp4";
                    skybox.SetInt( "_Rotation" ,  265);

                    videoID = 3;
                };
            } else if (videoID == 1){
                if (direction == "Left"){
                    videoPlayer.url = "Assets/Videos/ChoiceLLeft.mp4";
                    skybox.SetInt( "_Rotation" ,  272);

                    videoID = 1;

                    // videoPlayer.url = "Assets/Videos/ChoiceLRight.mp4";
                    // skybox.SetInt( "_Rotation" ,  281);
                }
                else if (direction == "Right"){
                    //play video right
                    videoPlayer.url = "Assets/Videos/ChoiceLRight.mp4";
                    skybox.SetInt( "_Rotation" ,  275);

                    videoID = 2;
                }
            }
           directionSet = false;
           elapsedTime = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(triggered = "In contact");
        StartCoroutine(mL);
    }

    private void OnTriggerExit(Collider other)
    {
        
        Debug.Log(triggered = "No longer in contact and direction is " + direction);
    
        StopCoroutine(mL);
        Destroy(mTile);
        Destroy(mChoice);
        directionSet = true;
    }

    private void spawnTile(){
        GameObject t = Instantiate(choiceTile) as GameObject;
        t.transform.position = new Vector3(-2.3f, -5.4f, 32.49f);
        t.name = "newChoiceTile";
    }

    IEnumerator measureLean(){
        measuring = true; 
        var controller = VZPlayer.Controller;
        var f1 = 0.2;
        var f2 = -0.2;
        shouldMove = false;
        var arrow = mTile.transform.Find("Arrow").GetComponent<SpriteRenderer>();
        platePos = mTile.transform.position;

        while ( measuring ) {
            mTile.transform.position = new Vector3 (platePos.x, platePos.y, platePos.z);
            if (controller.HeadRot > f1){
                direction = "Left";
                arrow.sprite = leftArrow;
            } else if ( controller.HeadRot < f2){
                direction = "Right";
                arrow.sprite = rightArrow;
            } else {
                direction = "Straight";
                arrow.sprite = defaultArrow;
            }
          yield return null;
        }
        measuring = false;
    }
}
