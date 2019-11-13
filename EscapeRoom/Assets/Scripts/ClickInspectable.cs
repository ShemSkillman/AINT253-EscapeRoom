using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickInspectable : Inspectable
{
    bool isActive = false;

    public override void ActivateInspection(bool isActive)
    {
        base.ActivateInspection(isActive);
        Cursor.visible = isActive;
        this.isActive = isActive;
    }

    private void Update()
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

                IControllable controllable = GetComponentInParent<IControllable>();
                if (controllable != null) controllable.sendButtonID(button.GetID());
            }
        }
    }
}
