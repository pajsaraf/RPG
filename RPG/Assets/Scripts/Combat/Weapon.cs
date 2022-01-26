﻿using UnityEngine;

namespace RPG.Combat 
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController animatorOveride = null;
        [SerializeField] GameObject equipedPrefab = null;
        [SerializeField] float weaponDamage = 5f;
        [SerializeField] float weaponRange = 2f;


        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equipedPrefab != null)
            {
                Instantiate(equipedPrefab, handTransform);
            }
            if (animatorOveride != null)
            {
                animator.runtimeAnimatorController = animatorOveride;
            }

        }

        public float GetDamage()
        {
            return weaponDamage;
        }
        public float GetRange()
        {
            return weaponRange;
        }


    }


}