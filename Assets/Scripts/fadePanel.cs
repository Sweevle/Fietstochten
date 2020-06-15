using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadePanel : MonoBehaviour
{
    public float Duration = 0.5f;
    private bool mFaded = false;
    public worldScript world;

    public void Fade(){
        var canvGroup = GameObject.Find("fadePanel").GetComponent<CanvasGroup>();

        StartCoroutine(DoFade(canvGroup, canvGroup.alpha, mFaded ? 1 : 0));

        mFaded = !mFaded;
        world.fadedOut = mFaded;
    }

    public IEnumerator DoFade (CanvasGroup canvGroup, float start, float end){
        float counter = 0f;

        while(counter < Duration){
            counter += Time.deltaTime;
            canvGroup.alpha = Mathf.Lerp(start, end, counter / Duration);

            yield return null;
        }
    }
}
