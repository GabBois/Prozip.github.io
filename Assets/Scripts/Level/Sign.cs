using System;
using Unity.Cinemachine;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable, IReceiver
{
    [SerializeField] private bool isActiveByDefault;
    [Space]
    [Header("References")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject interactTextObject;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject canvasObject;
    [SerializeField] private GameObject[] buttonObjects;

    [SerializeField] private float waitBeforeOpenDuration;
    
    readonly int OpenTriggerHash = Animator.StringToHash("Open");
    readonly int CloseTriggerHash = Animator.StringToHash("Close");
    readonly int IsActiveHash = Animator.StringToHash("IsActive");

    private bool isActive;
    
    private void Start()
    {
        interactTextObject.SetActive(false);
        canvasObject.SetActive(false);
        foreach (GameObject o in buttonObjects)
        {
            o.SetActive(false);
        }
        
        isActive = isActiveByDefault;
        anim.SetBool(IsActiveHash, isActive);
    }

    public bool Cancelable { get; set; } = true;

    public void Activate()
    {
        isActive = true;
        anim.SetBool(IsActiveHash, isActive);
    }

    public void Deactivate()
    {
        isActive = false;
        anim.SetBool(IsActiveHash, isActive);
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
        if (!isActive)
        {
            Debug.Log("No energy...");
            return;
        }
        cam.Priority = 11;
        GameEvents.TriggerInteractionStarted();
        Invoke(nameof(PlayOpen), waitBeforeOpenDuration);
    }

    void PlayOpen()
    {
        anim.SetTrigger(OpenTriggerHash);
    }

    public void ShowCanvas()
    {
        canvasObject.SetActive(true);
    }

    public void CancelInteraction()
    {
        cam.Priority = 0;
        GameEvents.TriggerInteractionEnded();
        anim.SetTrigger(CloseTriggerHash);
        canvasObject.SetActive(false);
    }

    public void ShowProjectPanel(int _index)
    {
        Debug.Log("Show Project Panel");
        MainUI.Instance.ShowProjectOverview(_index);
    }
}
