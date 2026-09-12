using System;
using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Debug.Log("C'est good!");
    }

    private void OnTriggerExit(Collider other)
    {
        if(!other.CompareTag("Player")) return;
        Debug.Log("C'est ciaooooooo!");
    }
}
