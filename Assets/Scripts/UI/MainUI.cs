using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance {get; private set;}
    [SerializeField] ProjectOverviewUI projectOverviewUI;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputModeController.Instance.closePanel += OnCancel;
        projectOverviewUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        InputModeController.Instance.closePanel -= OnCancel;
    }

    public void ShowProjectOverview(int _index)
    {
        InputModeController.Instance.EnableUI();
        projectOverviewUI.gameObject.SetActive(true);
        projectOverviewUI.Init(_index);
    }

    public void OnCancel()
    {
        HideProjectOverview();
    }
    
    public void HideProjectOverview()
    {
        InputModeController.Instance.DisableUI();
        projectOverviewUI.Cancel();
        projectOverviewUI.gameObject.SetActive(false);
    }
}
