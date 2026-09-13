using System;
using Unity.Cinemachine;
using UnityEngine;

public class Sign : MonoBehaviour, IInteractable
{
    [Header("References")]
    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private GameObject interactTextObject;
    

    private void Start()
    {
        interactTextObject.SetActive(false);
    }

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
        cam.Priority = 11;
    }
}
