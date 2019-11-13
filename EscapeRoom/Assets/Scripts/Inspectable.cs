using UnityEngine;

public class Inspectable : MonoBehaviour
{
    protected Camera inspectionCamera;

    private void Awake()
    {
        inspectionCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        ActivateInspection(false);
    }

    public virtual void ActivateInspection(bool isActive)
    {
        inspectionCamera.gameObject.SetActive(isActive);
    }

    public bool GetIsCameraActive()
    {
        return inspectionCamera.isActiveAndEnabled;
    }
}
