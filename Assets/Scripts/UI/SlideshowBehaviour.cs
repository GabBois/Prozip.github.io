using System;
using UnityEngine;

public class SlideshowBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] slideList;
    private int currentIndex = 0;

    private void Start()
    {
        foreach (GameObject slide in slideList)
        {
            slide.SetActive(false);
        }
        slideList[0].SetActive(true);
    }

    public void Next()
    {
        HideCurrentSlide();
        currentIndex++;
        if (currentIndex >= slideList.Length)
        {
            currentIndex = 0;
        }
        UpdateSlide();
    }

    public void Previous()
    {
        HideCurrentSlide();
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = slideList.Length - 1;
        }
        UpdateSlide();
    }

    void HideCurrentSlide()
    {
        slideList[currentIndex].SetActive(false);
    }
    
    void UpdateSlide()
    {
        slideList[currentIndex].SetActive(true);
    }
}
