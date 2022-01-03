
using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {  
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 3f;      //Delay between attack animations
        [SerializeField] float weaponDamage = 5f;



        Transform target;   
        float timeSinceLastAttack = 0;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return; 

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehaviour();
            }
        }

        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                // this will trigger the hit event bellow
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;


            }

        }
        //animnation event - for the moment in animation when the actual hit is occuring - called from animator not code
        void Hit()
        {
            Health healthComponent = target.GetComponent<Health>();
            healthComponent.TakeDamage(weaponDamage);
        }



        private bool GetIsInRange()
        {
            return Vector3.Distance(transform.position, target.position) < weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);  //using rpg.core namespace ctrl . win key
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }





    }
}


