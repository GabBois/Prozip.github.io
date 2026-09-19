using System;
using UnityEngine;
using UnityEngine.Events;

public class TriggerBehaviour : MonoBehaviour
{
    [SerializeField] private UnityEvent triggerEnterEvents;
    [SerializeField] private UnityEvent triggerExitEvents;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerEnterEvents.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            triggerExitEvents.Invoke();
        }
    }
}
