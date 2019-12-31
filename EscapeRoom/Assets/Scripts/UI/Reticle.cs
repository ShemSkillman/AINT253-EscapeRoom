using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EscapeRoom.UI
{
    public class Reticle : MonoBehaviour
    {
        [SerializeField] Image defaultReticlePrefab;
        [SerializeField] Image interactReticlePrefab;

        Image currentReticle;
        bool isDefault = false;

        private void Start()
        {
            SetDefault();
        }

        public void SetDefault()
        {
            if (isDefault) return;

            if (currentReticle != null) Destroy(currentReticle);
            currentReticle = Instantiate(defaultReticlePrefab, transform);
            isDefault = true;
        }

        public void SetInteract()
        {
            if (!isDefault) return;

            if (currentReticle != null) Destroy(currentReticle);
            currentReticle = Instantiate(interactReticlePrefab, transform);
            isDefault = false;
        }
    }
}

