using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;

namespace RPG.Control   //creating a name space to control dependencies
{
    public class PlayerController : MonoBehaviour
    {

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCurser();
            }
        }

        private void MoveToCurser()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            bool hasHit = Physics.Raycast(ray, out hit);
            if (hasHit)
            {
                GetComponent<Mover>().MoveTo(hit.point);
            }
        }

    }
}


