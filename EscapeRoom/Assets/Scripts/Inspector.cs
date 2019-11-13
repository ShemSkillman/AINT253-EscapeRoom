using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspector : MonoBehaviour
{
    Camera playerCamera;
    LayerMask interactablesLayer;
    Mover mover;

    [SerializeField] float maxInspectionDistance = 4f;
    [SerializeField] Canvas overlayCanvas;

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
        if (Input.GetKeyDown(KeyCode.E))
        {
            Vector3 rayOrigin = playerCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));

            if (Physics.Raycast(rayOrigin, playerCamera.transform.forward, out RaycastHit hit, maxInspectionDistance, interactablesLayer))
            {
                Inspectable inspectable = hit.collider.gameObject.GetComponentInParent<Inspectable>();

                if (inspectable == null) return;

                bool isInspecting = inspectable.GetIsCameraActive();

                if (isInspecting)
                {
                    mover.IsFrozen = false;
                    inspectable.ActivateInspection(false);
                    overlayCanvas.gameObject.SetActive(true);
                }
                else
                {
                    mover.IsFrozen = true;
                    inspectable.ActivateInspection(true);
                    overlayCanvas.gameObject.SetActive(false);
                }
            }
        }
    }
}
