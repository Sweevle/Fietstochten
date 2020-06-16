using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

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

    public VideoManager videoManager;
    private AudioSource audioSource;
    
    float next_spawn_time;
    float first_spawn_time;
    GameObject transitionCanvas;

    // Start is called before the first frame update
    void Start()
    {
        transitionCanvas = GameObject.Find("TransitionCanvas");
        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        tutorialDone = true;
        shouldMove = true;
        fadedOut = false;

        //elapsed time + directionchoice time from video
        next_spawn_time = elapsedTime + 5.0f;
        
    }

    // Update is called once per frame
    void Update(){
        var controller = VZPlayer.Controller;

        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        if (!transitionCanvasActive){
            videoManager.Play();
            if (fadedOut == true){
                videoManager.Pause();
            }
            
            if (!audioSource.isPlaying && fadedOut == false){
                var timeText = gameUI.transform.Find("Time").GetComponent<Text>();
                elapsedTime += Time.deltaTime;
                globalTime += Time.deltaTime;
                timeText.text = globalTime.ToString("f0");

                var speed = gameUI.transform.Find("Speed").GetComponent<Text>();
                var distance = gameUI.transform.Find("Distance").GetComponent<Text>();
                speed.text = (controller.InputSpeed * 3.6).ToString("f1");
                distance.text = controller.EstimatedDistance.ToString("f1");
                
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
        next_spawn_time += 15.0f;
        tutorialDone = true;
    }

    void skipTutorial(){
        spawnDirectionChoiceTile();
        //increment next_spawn_time
        next_spawn_time += 15.0f;
    }
}
