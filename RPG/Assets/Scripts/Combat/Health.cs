using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead = false;

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);  //healthPoints = (max health - damage) or 0 so health can never be less than zero
            print(healthPoints);
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
        }
    }



}
