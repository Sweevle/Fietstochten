using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeScreen : MonoBehaviour
{
    GameObject[] loopUI;
    public float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        loopUI = GameObject.FindGameObjectsWithTag("uitest");
        loopUI[0].SetActive(false);
        loopUI[1].SetActive(false);
        loopUI[2].SetActive(false);
        loopUI[3].SetActive(false);
        loopUI[4].SetActive(false);
        loopUI[5].SetActive(false);
        loopUI[7].SetActive(false);

        Debug.Log(loopUI[0]);
        Debug.Log(loopUI[1]);
        Debug.Log(loopUI[2]);
        Debug.Log(loopUI[3]);
        Debug.Log(loopUI[4]);
        Debug.Log(loopUI[5]);
        Debug.Log(loopUI[6]);
        Debug.Log(loopUI[7]);

    }

    // Update is called once per frame
    void Update()
    {

        elapsedTime += Time.deltaTime;


        if (elapsedTime > 30f){
            elapsedTime = 0;
            nextScreen();
        }


    }

    public void nextScreen(){
        if (loopUI[0].activeInHierarchy){
             loopUI[0].SetActive(false);
             loopUI[1].SetActive(true);
        } else
        if (loopUI[1].activeInHierarchy){
             loopUI[1].SetActive(false);
             loopUI[2].SetActive(true);
        } else 
        if (loopUI[2].activeInHierarchy){
             loopUI[2].SetActive(false);
             loopUI[3].SetActive(true);
        } else 
        if (loopUI[3].activeInHierarchy){
             loopUI[3].SetActive(false);
             loopUI[4].SetActive(true);
        } else 
        if (loopUI[4].activeInHierarchy){
             loopUI[4].SetActive(false);
             loopUI[5].SetActive(true);
        } else 
        if (loopUI[5].activeInHierarchy){
             loopUI[5].SetActive(false);
             loopUI[7].SetActive(true);
        } else 
        if (loopUI[6].activeInHierarchy){
             loopUI[6].SetActive(false);
             loopUI[0].SetActive(true);
        } else 
        if (loopUI[7].activeInHierarchy){
             loopUI[7].SetActive(false);
             loopUI[6].SetActive(true);
        }
        

    }

}
