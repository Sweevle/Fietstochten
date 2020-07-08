using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class setValues : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        var time = transform.Find("Time").GetComponent<Text>();
        var topSpeed = transform.Find("TopSpeed").GetComponent<Text>();
        var avgSpeed = transform.Find("AvgSpeed").GetComponent<Text>();
        var distance = transform.Find("Distance").GetComponent<Text>();
        var spins = transform.Find("TotalSpins").GetComponent<Text>();
        var rpm = transform.Find("Rpm").GetComponent<Text>();

        time.text = GlobalData.Instance.time;
        topSpeed.text = GlobalData.Instance.speed.ToString("f1");
        distance.text = GlobalData.Instance.distance.ToString("f2");
        spins.text = GlobalData.Instance.spins.ToString("f0");
        avgSpeed.text = GlobalData.Instance.avgSpeed.ToString("f1");
        rpm.text = GlobalData.Instance.rpm.ToString("f0");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
