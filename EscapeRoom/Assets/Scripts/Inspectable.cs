using UnityEngine;

public class Inspectable : MonoBehaviour
{
    Camera inspectionCamera;

    private void Awake()
    {
        inspectionCamera = GetComponentInChildren<Camera>();
    }

    private void Start()
    {
        ActivateCamera(false);
    }

    public void ActivateCamera(bool isActive)
    {
        inspectionCamera.gameObject.SetActive(isActive);
    }

    public bool GetIsCameraActive()
    {
        return inspectionCamera.isActiveAndEnabled;
    }
}
