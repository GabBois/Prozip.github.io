using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldButtonCensor : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;

    private WorldButton currentHover;
    private bool isActive = true;
    private Ray ray;

    private CinemachineBrain brain;

    private void Awake()
    {
        brain = GetComponent<CinemachineBrain>();
    }

    private void OnEnable()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(OnCameraUpdated);
    }

    private void OnCameraUpdated(CinemachineBrain _updatedBrain)
    {
        if (_updatedBrain != brain) return;
        DoRaycast();
    }

    public void SetActive(bool _state)
    {
        isActive = _state;
    }

    void DoRaycast()
    {
        if(!isActive)  return;
        
        WorldButton hitButton = null; 
        ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, layerMask))
        {
            hitButton = hit.collider.GetComponent<WorldButton>();
        }

        if (hitButton != currentHover)
        {
            currentHover?.SetHover(false);
            currentHover = hitButton;
            currentHover?.SetHover(true);
        }

        if (hitButton != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            hitButton.Click();
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(ray);
    }
}
