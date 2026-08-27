using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Selector : MonoBehaviour
{
    [SerializeField] private LayerMask mask;
    [SerializeField] private Camera cam;

    private RaycastHit hit;
    
    private void Update()
    {
        CheckSelectable();
    }

    void CheckSelectable()
    {
        if (Physics.Raycast(cam.ScreenPointToRay(Mouse.current.position.value), out hit,10f,mask))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
