using System;
using Unity.Cinemachine;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject interactTextObject;
    [SerializeField] private Animator anim;

    [SerializeField] private float waitBeforeOpenDuration;
    
    readonly int OpenTriggerHash = Animator.StringToHash("Open");
    readonly int CloseTriggerHash = Animator.StringToHash("Close");
    
    private void Start()
    {
        interactTextObject.SetActive(false);
    }

    public void ShowInfos()
    {
        interactTextObject.SetActive(true);
    }

    public void HideInfos()
    {
        interactTextObject.SetActive(false);
    }

    public void Interact()
    {
        cam.Priority = 11;
        GameEvents.TriggerInteractionStarted();
        Invoke(nameof(PlayOpen), waitBeforeOpenDuration);
    }

    void PlayOpen()
    {
        anim.SetTrigger(OpenTriggerHash);
    }

    public void CancelInteraction()
    {
        cam.Priority = 0;
        GameEvents.TriggerInteractionEnded();
        anim.SetTrigger(CloseTriggerHash);
    }
}
