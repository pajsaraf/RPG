using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {

        CanvasGroup canvasGroupFader;
        
        private void Awake() 
        {
            canvasGroupFader = GetComponent<CanvasGroup>();
            //StartCoroutine(FadeOutIn());    
        }

        public void FadeOutImmediate()
        {
            canvasGroupFader.alpha = 1;
        }

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