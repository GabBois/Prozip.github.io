using System;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

public class ProjectOverviewUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Volume volume;
    
    [SerializeField] private GameObject[] projectPanelList;
    private GameObject currentPanel;

    private void Start()
    {
        foreach (GameObject panel in projectPanelList)
        {
            panel.SetActive(false);
        }

        gameObject.SetActive(false);
    }

    public void Init(int _index)
    {
        volume.enabled = true;
        currentPanel = projectPanelList[_index];
        currentPanel.SetActive(true);
    }

    public void Cancel()
    {
        volume.enabled = false;
        currentPanel.SetActive(false);
    }
}
