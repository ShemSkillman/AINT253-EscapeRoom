using UnityEngine;

namespace EscapeRoom.Interact
{
    public class Interactable : MonoBehaviour
    {
        [SerializeField] protected Camera inspectionCamera;

        protected virtual void Awake()
        {
            inspectionCamera = GetComponentInChildren<Camera>();
        }

        protected virtual void Start()
        {
            ActivateInteraction(false);
        }

        public Camera ActivateInteraction(bool isActive)
        {
            Trigger(isActive);
            return inspectionCamera;
        }

        protected virtual void Trigger(bool isActive) { }

        public virtual bool IsInteractionValid()
        {
            return true;
        }
    }
}

