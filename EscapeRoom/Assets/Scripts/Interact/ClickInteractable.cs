using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInteractable : Interactable
{
    bool isActive = false;

    protected override void Trigger(bool isActive)
    {
        Cursor.visible = isActive;
        this.isActive = isActive;
    }

    protected void Update()
    {
        if (!isActive) return;

        Ray ray = inspectionCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                Button button = hit.collider.gameObject.GetComponentInParent<Button>();

                if (button == null) return;

                button.PushButton();

                SendButtonID(button.GetID());
            }
        }
    }

    protected virtual void SendButtonID(int id) { }
}
