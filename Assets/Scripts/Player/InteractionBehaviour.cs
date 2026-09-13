using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionBehaviour : MonoBehaviour
{
    [SerializeField] private Transform rayPoint;
    
    [Header("Settings")] [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask interactableLayer;
    private RaycastHit hit;
    IInteractable interactable;

    void OnInteract(InputValue _value)
    {
        if (interactable != null)
        {
            interactable.Interact();
        }
    }
    
    private void Update()
    {
        if (Physics.Raycast(rayPoint.position, rayPoint.forward, out hit, maxDistance, layerMask: interactableLayer))
        {
            interactable = hit.collider.GetComponent<IInteractable>();
            interactable?.ShowInfos();
        }
        else if(interactable != null)
        {
            interactable.HideInfos();
            interactable =  null;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(rayPoint.position, rayPoint.forward * maxDistance);
    }
}
