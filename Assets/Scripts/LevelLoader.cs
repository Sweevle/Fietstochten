using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 2f;

    // Update is called once per frame
    void Update()
    { 
        
    }

    public void LoadNextVideo(){
        StartCoroutine(LoadVideo());
    }


    IEnumerator LoadVideo(){
        //play animation
        transition.SetTrigger("Start");

        //wait
        yield return new WaitForSeconds(transitionTime);


        transition.SetTrigger("FadeIn");

    }
}
