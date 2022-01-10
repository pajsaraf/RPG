//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control   //creating a name space to control dependencies
{
    public class PlayerController : MonoBehaviour
    {
        Health health;

        private void Start() 
        {
            health = GetComponent<Health>();
        }

        private void Update()
        {
            if (health.IsDead()) return; // if its dead then dont do anything

            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());   // shoot raycast to look for a target in priority and if none found move instead
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null) continue;  

                //GameObject tagetGameObject = target.gameObject; // go to fighter script and take the gameobject we call target
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))   // if we cant attack cause the target is dead
                {
                    continue;  // then send raycast through the dead
                }

                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if(Input.GetMouseButton(0))
                {
                    //GetComponent<Mover>().MoveTo(hit.point);
                    GetComponent<Mover>().StartMoveAction(hit.point, 1f);  // 1f means full speed of enemy
                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}


