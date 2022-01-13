using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad = -1;  // this will let me pick the scene at the editor level AND give errors if we dont put anything
        // need to go to editor File >> Build settings and look at the index of the scenes, then use that number in the editor 
        private void OnTriggerEnter(Collider other) 
        {
            //print("Going Inside");
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }

}
