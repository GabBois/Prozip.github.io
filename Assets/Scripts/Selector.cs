using System;
using UnityEngine;

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
        if (Physics.Raycast(cam.ScreenPointToRay(Input.mousePosition), mask))
        {
            Debug.Log(hit.collider.gameObject.name);
        }
    }
}
