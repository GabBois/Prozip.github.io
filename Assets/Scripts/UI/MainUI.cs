using System;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public static MainUI Instance {get; private set;}
    [SerializeField] ProjectOverviewUI projectOverviewUI;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowProjectOverview(int _index)
    {
        projectOverviewUI.gameObject.SetActive(true);
        projectOverviewUI.Init(_index);
    }
}
