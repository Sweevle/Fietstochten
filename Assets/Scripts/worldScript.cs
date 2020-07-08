using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class worldScript : MonoBehaviour
{

    public float elapsedTime;
    public float globalTime;
    public bool transitionCanvasActive;
    public bool tutorialDone;
    public bool shouldMove;
    public bool fadedOut;
    
    public GameObject directionChoiceTile;
    public GameObject gameUI;

    private VideoPlayer videoPlayer;
    public VideoManager videoManager;
    private AudioSource audioSource;
    
    public float next_spawn_time;
    float first_spawn_time;
    GameObject transitionCanvas;
    private float maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        transitionCanvas = GameObject.Find("TransitionCanvas");
        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        tutorialDone = true;
        shouldMove = true;
        fadedOut = false;

        // videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        // videoPlayer.url = "file://mnt/sdcard/Videos/1.mp4";
        // videoPlayer.Prepare();
        //elapsed time + directionchoice time from video
        // if (tutorialDone == false){
        //     next_spawn_time = elapsedTime + ((float)videoPlayer.clip.length - audioSource.clip.length - 11f);
        //     // Debug.Log(next_spawn_time);
        // } else{
        //     next_spawn_time = elapsedTime + ((float)videoPlayer.clip.length - 11f);
        //     // Debug.Log(next_spawn_time);
        // }

        next_spawn_time = elapsedTime + 15f;
    }

    // Update is called once per frame
    void Update(){
        var controller = VZPlayer.Controller;

        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        if (!transitionCanvasActive){
            videoManager.Play();

            // if (elapsedTime > 300f){

            //     GlobalData.Instance.spins = controller.Spins;
            //     GlobalData.Instance.speed = maxSpeed;
            //     GlobalData.Instance.avgSpeed = 5.00f;
            //     GlobalData.Instance.rpm = (controller.Spins / (elapsedTime / 60));
            //     GlobalData.Instance.time = globalTime;
            //     GlobalData.Instance.distance = controller.EstimatedDistance;

            //     SceneManager.LoadScene("resultScreen");
            // }

            if (fadedOut == true){
                videoManager.Pause();
            }
            
            if (fadedOut == false){
                var timeText = gameUI.transform.Find("Time").GetComponent<Text>();
                elapsedTime += Time.deltaTime;
                globalTime += Time.deltaTime;
                timeText.text = elapsedTime.ToString("f0");

                var speed = gameUI.transform.Find("Speed").GetComponent<Text>();
                var distance = gameUI.transform.Find("Distance").GetComponent<Text>();
                speed.text = (controller.InputSpeed * 3.6).ToString("f1");
                distance.text = controller.EstimatedDistance.ToString("f2");
                
                if ( (controller.InputSpeed * 3.6) > maxSpeed){
                    maxSpeed = (float)(controller.InputSpeed * 3.6);
                }
            }

            if (elapsedTime > next_spawn_time){
                if (tutorialDone == false){
                    playTutorial();
                    // Debug.Log("tutorialDone in update" + tutorialDone);
                } else if (tutorialDone == true){
                    skipTutorial();
                }
            }
        }
    }

    void spawnDirectionChoiceTile(){
        var t = Instantiate(directionChoiceTile,new Vector3(-2.3f, -5.4f, 31.49f), Quaternion.identity);
        t.name = "directionChoiceTile";
    }

    void setTutorialDone()
    {
        tutorialDone = true;
    }

    void playTutorial(){

        audioSource.Play();
        Invoke("spawnDirectionChoiceTile", audioSource.clip.length);
        next_spawn_time += 32.0f;
        tutorialDone = true;
    }

    void skipTutorial(){
        spawnDirectionChoiceTile();
        //increment next_spawn_time
        next_spawn_time += 20.0f;
    }
}
