using EscapeRoom.Core;
using EscapeRoom.Interact;
using System;
using UnityEngine;

namespace EscapeRoom.Item
{
    public class Safe : ClickInteractable
    {
        [SerializeField] float openDelay = 2f;

        Animator animator;

        bool unlocked = false;
        string codeCombination;
        string attempt = "";

        public delegate void OnObjectiveComplete(Objective objective);
        public event OnObjectiveComplete onSafeOpen;

        protected override void Awake()
        {
            base.Awake();
            animator = GetComponent<Animator>();
            GenerateRandomCode();
        }

        private void GenerateRandomCode()
        {
            int code = UnityEngine.Random.Range(0, 10000);

            codeCombination = code.ToString();

            while(codeCombination.Length < 4)
            {
                codeCombination = "0" + codeCombination;
            }
        }

        protected override void SendButtonID(int id)
        {
            if (unlocked) return;

            attempt += id.ToString();

            if (attempt == codeCombination)
            {
                unlocked = true;
                Invoke("OpenSafe", openDelay);
                onSafeOpen(Objective.EnterSafeCode);
                return;
            }
            else if (attempt.Length >= codeCombination.Length)
            {
                attempt = "";
            }
        }

        protected void OpenSafe()
        {
            animator.SetTrigger("openSafe");
        }

        public string GetCode()
        {
            return codeCombination;
        }

    }
}

