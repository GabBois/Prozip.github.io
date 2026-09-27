using System;
using System.Collections;
using UnityEngine;

public class URLLauncher : MonoBehaviour, IInteractable
{
    [SerializeField] private string url;
    [SerializeField] private float sleepTimeBeforeOpen;
    [Space] [SerializeField] private GameObject infoObject;

    public static event Action OnURLOpened;
    public bool Cancelable { get; set; }

    private void Start()
    {
        infoObject.SetActive(false);
    }

    public void ShowInfos()
    {
        infoObject.SetActive(true);
    }

    public void HideInfos()
    {
        infoObject.SetActive(false);
    }

    public void Interact()
    {
        StartCoroutine(nameof(OpenURL));
    }

    private IEnumerator OpenURL()
    {
        OnURLOpened?.Invoke();
        yield return new WaitForSeconds(sleepTimeBeforeOpen);
        
        StartURL();
    }

    public void CancelInteraction()
    {
        
    }

    [ContextMenu("OpenURL")]
    public void StartURL()
    {
        System.Diagnostics.Process.Start(url);
    }
}
