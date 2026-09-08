using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private LayerMask nodeLayer; // Layer des nodes
    [SerializeField] private Camera cam;

    private NodeBehaviour currentHoveredNode;
    private bool isMouseOverNode = false;

    private void Update()
    {
        // Raycast de sécurité (pour éviter les faux positifs en 3D)
        Ray ray = cam.ScreenPointToRay(Mouse.current.position.value);
        bool isRaycastingNode = Physics.Raycast(ray, out RaycastHit hit, 10f, nodeLayer);

        // Si le Raycast touche une node, on vérifie si la souris est dessus
        if (isRaycastingNode)
        {
            NodeBehaviour node = hit.collider.GetComponent<NodeBehaviour>();
            if (node != null)
            {
                // On force l'état "hover" si le Raycast touche la node
                if (!isMouseOverNode)
                {
                    node.OnHoverEnter();
                    currentHoveredNode = node;
                    isMouseOverNode = true;
                }
            }
        }
        else
        {
            // Si le Raycast ne touche plus la node, on déclenche OnHoverExit
            if (isMouseOverNode && currentHoveredNode != null)
            {
                currentHoveredNode.OnHoverExit();
                currentHoveredNode = null;
                isMouseOverNode = false;
            }
        }
    }
}