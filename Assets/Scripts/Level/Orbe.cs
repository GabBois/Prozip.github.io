using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public class Orbe : Grabbable, IInteractable
{
    [SerializeField] private GameObject interactTextObject;
    [FormerlySerializedAs("collider")] [SerializeField] private Collider col;
    [SerializeField] private Rigidbody rb;
    public bool Cancelable { get; set; }
    
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
        
    }

    public void CancelInteraction()
    {
        
    }

    public override void EnableGrabbing()
    {
        col.enabled = false;
        rb.isKinematic = true;
    }

    public override void DisableGrabbing()
    {
        col.enabled = true;
        rb.isKinematic = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            ShowInfos();
            other.TryGetComponent(out PlayerGrabber grabber);
            grabber.GetObjectToGrab(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInfos();
            other.TryGetComponent(out PlayerGrabber grabber);
            grabber.LoseObject();
        }
    }
}
