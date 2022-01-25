using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;
using RPG.Saving;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] Transform target;
        [SerializeField] float maxSpeed = 6f;
        
        NavMeshAgent navMeshAgent;
        Ray lastRay;  //last ray shot at screen - no longer used here
        Health health;


        private void Start() 
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {
            UpdateAnimator();
            navMeshAgent.enabled = !health.IsDead();

        }

        public void StartMoveAction(Vector3 destination, float speedFraction) 
        {
            GetComponent<ActionScheduler>().StartAction(this);  //using rpg.core namespace ctrl . win key
            
            //GetComponent<Fighter>().Cancel();  depricate from 32 on - replaced by IAction interface script
            MoveTo(destination, speedFraction);
        }



        public void MoveTo(Vector3 destination, float speedFraction)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);  //the clamp is between 0 and 100% of maxSpeed
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }


        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;  // get the speed from navmesh component
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);  //make velocity variable into  a local variable to find just the speed and not the global positioning
            float speed = localVelocity.z;
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }

        //ISavable - 2 states has an object requirement so any c#object = capture anything - restore same thing
        public object CaptureState()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data["position"] = new SerializableVector3(transform.position);
            data["rotation"] = new SerializableVector3(transform.eulerAngles);
            return data;
            //return new SerializableVector3(transform.position);

        }

        public void RestoreState(object state)  //called before start
        {
            //SerializableVector3 position = (SerializableVector3)state; //casting the object state as a vector 3 - will get errors if n ot vector 3 is present
            Dictionary<string, object> data = (Dictionary<string, object>)state;
            GetComponent<NavMeshAgent>().enabled = false; //cancel navmeshagent to avoid errors
            //transform.position = position.ToVector();  // move to the vector3 position
            transform.position = ((SerializableVector3)data["position"]).ToVector();
            transform.eulerAngles = ((SerializableVector3)data["rotation"]).ToVector();
            GetComponent<NavMeshAgent>().enabled = true; //restore navmesh agent

        }
    }
}


