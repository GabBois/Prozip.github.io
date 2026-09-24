using System;
using System.Collections;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class ProjectOverviewUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Volume volume;

    [Space] [SerializeField] private Canvas worldCanvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private GameObject[] projectPanelList;
    [SerializeField] private float fadeInDuration;
    private GameObject currentPanel;
    [SerializeField] private UnityEvent onCancel;

    private Coroutine fadeInRoutine;
    private Coroutine fadeOutRoutine;

    private void Awake()
    {
        foreach (GameObject panel in projectPanelList)
        {
            panel.SetActive(false);
        }

        canvasGroup.alpha = 0f;
        // SetRaycastsActive(false);
        // gameObject.SetActive(false);
    }

    public void Init(int _index)
    {
        volume.enabled = true;
        currentPanel = projectPanelList[_index];
        if(fadeInRoutine != null) StopCoroutine(fadeInRoutine);
        
        fadeInRoutine = StartCoroutine(nameof(FadeIn));
        currentPanel.SetActive(true);
    }

    public void Cancel()
    {
        volume.enabled = false;
        if(fadeOutRoutine != null) StopCoroutine(fadeOutRoutine);
        
        fadeOutRoutine = StartCoroutine(nameof(FadeOut));
        onCancel.Invoke();
    }
    
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        float alpha = 0f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime +=  Time.deltaTime;
            alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;
            
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        float alpha = 1f;
        while (elapsedTime < fadeInDuration)
        {
            elapsedTime +=  Time.deltaTime;
            alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeInDuration);
            canvasGroup.alpha = alpha;

            yield return null;
        }

        currentPanel.SetActive(false);
        Canvas.ForceUpdateCanvases();
        StartCoroutine(nameof(RepairWorldCanvas));
        // gameObject.SetActive(false);
    }
    
    private IEnumerator RepairWorldCanvas()
    {
        yield return null; // attendre la fin de frame

        var worldRaycaster = worldCanvas.GetComponent<GraphicRaycaster>();
        worldRaycaster.enabled = false;
        yield return null;
        worldRaycaster.enabled = true;
    }
}
