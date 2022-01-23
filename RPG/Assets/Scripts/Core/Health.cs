using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public bool IsDead()
        {
            return isDead;
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);  //healthPoints = (max health - damage) or 0 so health can never be less than zero
            //print(healthPoints);
            if (healthPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            if (isDead) return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();

        }

        //ISaveable interface 
        public object CaptureState()
        {
            return healthPoints;   //healthPoints is a float so its an easy thing to save in retrun     
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;  // set healthpoint state by casting state as a float

            if (healthPoints <= 0)  //enemies stay dead on load
            {
                Die();
            }
        }

    }



}
