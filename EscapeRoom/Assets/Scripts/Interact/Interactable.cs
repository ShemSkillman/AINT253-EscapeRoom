using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] protected Camera inspectionCamera;

    protected virtual void Awake()
    {
        inspectionCamera = GetComponentInChildren<Camera>();
    }

    protected void Start()
    {
        ActivateInteraction(false);
    }

    public Camera ActivateInteraction(bool isActive)
    {
        Trigger(isActive);
        return inspectionCamera;
    }

    protected virtual void Trigger(bool isActive){ }
}
