using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class PersistentObjectSpawner : MonoBehaviour
    {
        [SerializeField] GameObject persistentObjectsPrefab;

        static bool hasSpawned = false;  //static variables are always on so very heavy - why not use a singleton?

        private void Awake() 
        {
            if (hasSpawned) return;
            
            SpawnPersistentObject();
            hasSpawned = true;

        }

        private void SpawnPersistentObject()
        {
            GameObject persistentObjects  = Instantiate(persistentObjectsPrefab);
            DontDestroyOnLoad(persistentObjects);
        }
    }

}

