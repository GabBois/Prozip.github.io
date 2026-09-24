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
    [SerializeField] private GameObject[] slideList;

    [SerializeField] private float waitBeforeOpenDuration;
    
    readonly int OpenTriggerHash = Animator.StringToHash("Open");
    readonly int CloseTriggerHash = Animator.StringToHash("Close");
    readonly int IsActiveHash = Animator.StringToHash("IsActive");

    private bool isActive;

    private int currentPanelIndex = 0;

    public event Action<int> OnPanelChanged;
    public event Action<int> OnPanelOpened;
    
    private void Start()
    {
        interactTextObject.SetActive(false);
        
        foreach (GameObject o in buttonObjects)
        {
            o.SetActive(false);
        }
        
        isActive = isActiveByDefault;
        anim.SetBool(IsActiveHash, isActive);
        OnPanelChanged?.Invoke(currentPanelIndex);
        canvasObject.SetActive(false);
    }

    #region IRECEIVER
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
    #endregion
    
    #region IINTERACTABLE
    public bool Cancelable { get; set; } = true;
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
    #endregion

    void PlayOpen()
    {
        anim.SetTrigger(OpenTriggerHash);
    }

    public void ShowCanvas()
    {
        canvasObject.SetActive(true);
        OnPanelChanged?.Invoke(currentPanelIndex);
    }

    public void CancelInteraction()
    {
        cam.Priority = 0;
        GameEvents.TriggerInteractionEnded();
        anim.SetTrigger(CloseTriggerHash);
        canvasObject.SetActive(false);
    }

    public void NextPanel()
    {
        currentPanelIndex++;
        if (currentPanelIndex >= buttonObjects.Length)
        {
            currentPanelIndex = 0;
        }
        OnPanelChanged?.Invoke(currentPanelIndex);
        Debug.Log($"Next Panel {currentPanelIndex}");
    }

    public void PreviousPanel()
    {
        currentPanelIndex--;
        if (currentPanelIndex < 0)
        {
            currentPanelIndex = buttonObjects.Length - 1;
        }
        OnPanelChanged?.Invoke(currentPanelIndex);
        Debug.Log($"Previous Panel {currentPanelIndex}");
    }
    
    public void ShowProjectPanel()
    {
        Debug.Log("Show Project Panel");
        OnPanelOpened?.Invoke(currentPanelIndex);
        // MainUI.Instance.ShowProjectOverview(_index);
    }
}
