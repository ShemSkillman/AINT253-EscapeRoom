﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeRoom.Interact.Item
{
    public class Button : MonoBehaviour
    {
        [SerializeField] int id = 0;

        // Cache references
        Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void PushButton()
        {
            animator.SetTrigger("buttonPush");
        }

        public int GetID()
        {
            return id;
        }
    }
}

