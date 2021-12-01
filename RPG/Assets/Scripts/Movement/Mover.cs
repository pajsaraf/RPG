using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour
    {
        [SerializeField] Transform target;

        Ray lastRay;  //last ray shot at screen


        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
        }



        public void MoveTo(Vector3 destination)
        {
            GetComponent<NavMeshAgent>().destination = destination;
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;  // get the speed from navmesh component
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  //make velocity variable into  a local variable to find just the speed and not the global positioning
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}


