using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {

        [SerializeField] float chaseDistance = 5f;
        
        Fighter fighter;
        GameObject player;
        Health health;

        private void Start() 
        {
            fighter = GetComponent<Fighter>();
            health = GetComponent<Health>();
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (health.IsDead()) return; // if its dead then dont do anything

            if (InAttackRangeOfPlayer() && fighter.CanAttack(player)) //distance between player and enemy - and Player is alive
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }

        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer < chaseDistance;
        }
    }


}