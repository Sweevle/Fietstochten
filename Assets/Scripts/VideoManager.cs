using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    private void Start(){
        videoPlayer = GetComponent<VideoPlayer>();
    }

    // Update is called once per frame
    public void Play()
    {
        videoPlayer.Play();
    }
    
    public void Pause()
    {
        videoPlayer.Pause();
    }

    public void Stop()
    {
        videoPlayer.Stop();
    }
}
