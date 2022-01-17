using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroupFader;
        
        private void Start() 
        {
            canvasGroupFader = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());    
        }
/*
        IEnumerator FadeOutIn()
        {
            yield return FadeOut(3f); // wait for coroutine FadeOut is finished
            print ("FadeOut");
            yield return FadeIn(1f);
            print("FadeIn");
        }  */

        public IEnumerator FadeOut (float time) 
        {
            while (canvasGroupFader.alpha < 1) 
            {
                canvasGroupFader.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time)
        {
            while (canvasGroupFader.alpha > 0)
            {
                canvasGroupFader.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }

    }
}