using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E 
        }

        [SerializeField] int sceneToLoad = -1;  // this will let me pick the scene at the editor level AND give errors if we dont put anything
        // need to go to editor File >> Build settings and look at the index of the scenes, then use that number in the editor 
        [SerializeField] Transform Spawnpoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 1f;
        [SerializeField] float fadeInTime = 1f;
        [SerializeField] float fadeWaitTime = 1f;
        
        private void OnTriggerEnter(Collider other) 
        {
            //print("Going Inside");
            if (other.tag == "Player")
            {
                StartCoroutine (Transition());
            }
        }

        private IEnumerator Transition()
        {
            if (sceneToLoad < 0)
            {
                Debug.LogError("You forgot to set the scene to load");
                yield break;    
            }

            DontDestroyOnLoad(gameObject);    //do not destroy the portal

            Fader fader = FindObjectOfType<Fader>();   //fadeout
            yield return fader.FadeOut(fadeOutTime);
            yield return SceneManager.LoadSceneAsync(sceneToLoad);  // get the id of the last portal
            //print("scene loaded");
            
            Portal otherPortal = GetOtherPortal();
            UpdatePlayer (otherPortal);

            yield return new WaitForSeconds(fadeWaitTime); //waiting in white screen for camera to stabilize
            yield return fader.FadeIn(fadeInTime);  //fade back

            Destroy(gameObject);  // destroy the old portal
        }

        private void UpdatePlayer(Portal otherPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(otherPortal.Spawnpoint.position);
            //player.transform.position = otherPortal.Spawnpoint.position;
            player.transform.rotation = otherPortal.Spawnpoint.rotation;

        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>()) // find all active of portals in variable portal
            {
                if (portal == this) continue; //skip the rest of for loop on variable this
                if(portal.destination != destination) continue; // only go to thew portal with correct enum destination

                return portal;   // return the current portal
            }
            return null; // in case no portals are found
        }
    }



}
