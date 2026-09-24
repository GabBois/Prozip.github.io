using System;
using UnityEngine;

public class SlideshowBehaviour : MonoBehaviour
{
    [SerializeField] private Sign sign;
    [SerializeField] private GameObject[] slideList;
    private int currentIndex = 0;
    
    private void OnEnable()
    {
        sign.OnPanelChanged += UpdatePanel;
        sign.OnPanelOpened += OpenPanel;
        foreach (GameObject o in slideList)
        {
            o.SetActive(false);
        }
    }
    
    private void OpenPanel(int _index)
    {
        MainUI.Instance.ShowProjectOverview(_index);
    }

    private void OnDisable()
    {
        sign.OnPanelChanged -= UpdatePanel;
        sign.OnPanelOpened -= OpenPanel;
    }

    private void UpdatePanel(int _index)
    {
        slideList[currentIndex].SetActive(false);
        currentIndex = _index;
        slideList[currentIndex].SetActive(true);
        Debug.Log($"UpdatePanel {currentIndex}");
    }
}
