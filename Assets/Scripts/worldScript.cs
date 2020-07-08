using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class worldScript : MonoBehaviour
{

    public float elapsedTime;
    public float globalTime;
    public float next_spawn_time;
    public bool transitionCanvasActive;
    public bool tutorialDone;
    public bool shouldMove;
    public bool fadedOut;
    public Text currentMinutes;
    public Text currentSeconds;
    public AudioClip bells;
    
    public GameObject directionChoiceTile;
    public GameObject gameUI;
    public VideoManager videoManager;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;
    private float maxSpeed;

    GameObject transitionCanvas;
    float first_spawn_time;
    
    // Start is called before the first frame update
    void Start()
    {
        //setting the bools.
        tutorialDone = false;
        shouldMove = true;
        fadedOut = false;


        transitionCanvas = GameObject.Find("TransitionCanvas");
        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        audioSource = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        
        // set starting video.
        videoPlayer = GameObject.Find("Video Player").GetComponent<VideoPlayer>();
        videoPlayer.url = Path.Combine(Application.streamingAssetsPath, "1.mp4");
        videoPlayer.Prepare();


        // check if the tutorial needs to be played, if yes then also take that in account for when the
        // first spawn time is for the direction choice
        if (tutorialDone == false){
            next_spawn_time = elapsedTime + (41f - 26f - 10f);
            // next_spawn_time = 0f;
            // Debug.Log(next_spawn_time);
        } else{
            next_spawn_time = elapsedTime + (41f - 15f);
            // Debug.Log(next_spawn_time);
        }
    }

    // Update is called once per frame
    void Update(){

        var controller = VZPlayer.Controller;
        transitionCanvasActive = transitionCanvas.activeInHierarchy;
        // if the transition canvas is active don't do anything.
        if (!transitionCanvasActive){
            videoManager.Play();


            // if the time reaches 5 minutes stop the tour, save the data and go to the result screen
            if (globalTime > 217f){
                string minutes = Mathf.Floor((int)globalTime / 60).ToString("00");
                string seconds = ((int)globalTime % 60).ToString("00");

                GlobalData.Instance.spins = controller.Spins;
                GlobalData.Instance.speed = maxSpeed;
                GlobalData.Instance.avgSpeed = 5.00f;
                GlobalData.Instance.rpm = (controller.Spins / (Mathf.Floor((int)globalTime / 60)));
                GlobalData.Instance.time = minutes + " : " + seconds;
                GlobalData.Instance.distance = controller.EstimatedDistance;
                
                SceneManager.LoadScene("resultScreen");
            }


            // when transitioning to another video pause the video
            if (fadedOut == true){
                // videoManager.Pause();
            }
            
            // set and update the data shown in the bar during the tour
            if (fadedOut == false){
                var timeText = gameUI.transform.Find("Time").GetComponent<Text>();
                elapsedTime += Time.deltaTime;
                globalTime += Time.deltaTime;
                
                string minutes = Mathf.Floor((int)globalTime / 60).ToString("00");
                string seconds = ((int)globalTime % 60).ToString("00");
                                
                timeText.text = minutes + " : " + seconds;



                var speed = gameUI.transform.Find("Speed").GetComponent<Text>();
                var distance = gameUI.transform.Find("Distance").GetComponent<Text>();
                speed.text = (controller.InputSpeed * 3.6).ToString("f1");
                distance.text = controller.EstimatedDistance.ToString("f2");
                
                if ( (controller.InputSpeed * 3.6) > maxSpeed){
                    maxSpeed = (float)(controller.InputSpeed * 3.6);
                }
            }


            // if the elapsed time hits the spawn time check if the tutorial has been done
            // else skip tutorial
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

    // spawn the object that starts the direction choice
    void spawnDirectionChoiceTile(){
        var t = Instantiate(directionChoiceTile,new Vector3(-2.3f, -5.4f, 31.49f), Quaternion.identity);
        t.name = "directionChoiceTile";
    }

    void setTutorialDone()
    {
        tutorialDone = true;
    }


    // play the audio tutorial and after spawn the object.
    // set the tutorial boolean to true
    void playTutorial(){
        var time = audioSource.clip.length;

        StartCoroutine(playtutorialSound());
        Invoke("spawnDirectionChoiceTile", 26f);
        next_spawn_time += 60.0f;
        tutorialDone = true;
    }


    // if the tutorial has been done, spawn object immediately.
    void skipTutorial(){
        audioSource.Play();
        Invoke("spawnDirectionChoiceTile", audioSource.clip.length);

        //increment next_spawn_time
        next_spawn_time += 60.0f;
    }

    IEnumerator playtutorialSound()
         {
             audioSource.Play();
             yield return new WaitForSeconds(audioSource.clip.length);
             audioSource.volume = 0.4f;
             audioSource.clip = bells;
             audioSource.Play();
         }
}
