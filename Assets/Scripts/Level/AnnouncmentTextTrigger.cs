using System;
using UnityEngine;

public class AnnouncmentTextTrigger : MonoBehaviour
{
    [SerializeField] private string textContent;
    [SerializeField] private bool onlyOnce;
    private bool alreadyTriggered;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (onlyOnce && !alreadyTriggered || !onlyOnce)
            {
                GameEvents.TriggerAnnouncmentTriggered(textContent);
                alreadyTriggered = true;
            }
        }
    }
}
