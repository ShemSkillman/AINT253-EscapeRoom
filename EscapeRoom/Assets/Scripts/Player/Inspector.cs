using EscapeRoom.Interact;
using EscapeRoom.UI;
using UnityEngine;

namespace EscapeRoom.Player
{
    public class Inspector : MonoBehaviour
    {
        Camera playerCamera;
        LayerMask interactablesLayer;
        Mover mover;
        Camera currentInspectionCamera = null;

        [SerializeField] float maxInspectionDistance = 4f;
        [SerializeField] Canvas overlayCanvas = null;
        [SerializeField] Reticle reticle;

        private void Awake()
        {
            playerCamera = GetComponentInChildren<Camera>();
            mover = GetComponent<Mover>();
        }

        private void Start()
        {
            interactablesLayer = LayerMask.GetMask("Interactable");
        }

        void Update()
        {
            Interactable interactable = null;
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out RaycastHit hit, maxInspectionDistance, interactablesLayer))
            {
                interactable = hit.collider.gameObject.GetComponentInParent<Interactable>();

                if (interactable == null)
                {
                    reticle.SetDefault();
                    return;
                }
                else if (!interactable.IsInteractionValid())
                {
                    //reticle error
                    return;
                }

                reticle.SetInteract();
            }
            else
            {
                reticle.SetDefault();
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (currentInspectionCamera != null) // Exits current camera view
                {
                    IsFixedCameraView(false);
                    currentInspectionCamera = null;
                }
                else if (interactable != null)
                {
                    currentInspectionCamera = interactable.ActivateInteraction(true);
                    if (currentInspectionCamera != null) IsFixedCameraView(true);
                }
            }

        }

        // Freezes player movement, removes aiming reticle and activates camera when interacting with object 
        // that has a camera
        public void IsFixedCameraView(bool isFixedView)
        {
            mover.IsFrozen = isFixedView;
            overlayCanvas.gameObject.SetActive(!isFixedView);

            if (isFixedView)
            {
                currentInspectionCamera.depth = 1;
            }
            else
            {
                currentInspectionCamera.depth = -1;
            }

        }
    }
}

