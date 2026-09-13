using System;
using UnityEngine;

public class SimpleButton : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject infoObject;

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
        Debug.Log($"[SimpleButton] T'as appuyé wesshhhh");
    }

    public void CancelInteraction()
    {
        throw new NotImplementedException();
    }
}
