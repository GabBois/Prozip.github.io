using System;
using UnityEngine;
using UnityEngine.InputSystem;

public sealed class InputModeController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    public static InputModeController Instance{get; private set;}

    private InputAction cancelAction;

    public event Action closePanel;
    
    private void Awake()
    {
        Instance = this;
        cancelAction = playerInput.actions["UI/Cancel"];
        
    }

    private void Start()
    {
    }

    private void OnEnable()
    {
        cancelAction.performed += ClosePanelUI;
    }

    private void OnDisable()
    {
        cancelAction.performed -= ClosePanelUI;
    }

    private void ClosePanelUI(InputAction.CallbackContext _obj)
    {
        closePanel?.Invoke();
    }


    public void EnableUI()
    {
        playerInput.SwitchCurrentActionMap("UI");
        Debug.Log(
            $"Map active : {playerInput.currentActionMap?.name}"
        );
    }

    public void DisableUI()
    {
        playerInput.SwitchCurrentActionMap("Player");
        Debug.Log(
            $"Map active : {playerInput.currentActionMap?.name}"
        );
    }
}
