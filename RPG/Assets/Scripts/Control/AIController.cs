using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;
using RPG.Movement;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;

        Fighter fighter;
        GameObject player;
        Mover mover;
        Health health;

        Vector3 guardPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");

            guardPosition = transform.position;  //ai guards the area it starts at
          
        }

        private void Update()
        {
            if (health.IsDead()) return; // if its dead then dont do anything

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) //distance between player and enemy - and Player is alive
            {
                timeSinceLastSawPlayer = 0;
                AttackBehaviour();
            }

            else if (timeSinceLastSawPlayer < suspicionTime) //suspicious guard state waits at Last LOS position
            {
                SuspicionBehaviour();
            }

            else  //return to starting position
            {
                GuardBehaviour();
            }

            timeSinceLastSawPlayer += Time.deltaTime;

        }

        private void GuardBehaviour()
        {
            mover.StartMoveAction(guardPosition);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction(); 
        }

        private void AttackBehaviour()
        {
            fighter.Attack(player);
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }

        //called by unity in scene view
        private void OnDrawGizmosSelected() 
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);

        }


    }


}