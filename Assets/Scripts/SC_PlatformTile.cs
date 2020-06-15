using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_PlatformTile : MonoBehaviour
{
    public float movingSpeed = 6;
    public worldScript world;
    private Transform arrow;
    private Transform choiceTime; 

    // Start is called before the first frame update
    void Start()
    {

        arrow = gameObject.transform.Find("Arrow");
        choiceTime = gameObject.transform.Find("ChoiceTime");
        world = GameObject.Find("World").GetComponent<worldScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (world.shouldMove){
           arrow.transform.Translate(Vector3.back * Time.deltaTime * movingSpeed, Space.World);
        }
        choiceTime.transform.Translate(Vector3.back * Time.deltaTime * movingSpeed, Space.World);
    }

    

}