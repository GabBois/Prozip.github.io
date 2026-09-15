using System;
using System.Threading.Tasks;
using UnityEngine;

public class EnergyRelay : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactTextObject;
    [SerializeField] private Transform sourcePoint;
    [SerializeField] private float sourceRadius;

    [Header("-[ Drop Sequence ]- ")]
    [SerializeField] private float dropTime;
    [SerializeField] private float sleepTimeBeforeActivation;
    [SerializeField] private float energyUpdateTime;

    private PlayerGrabber grabber;
    private EnergySource currentSource;
    
    private void Start()
    {
        interactTextObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowInfos();
            if(other.TryGetComponent(out InteractionBehaviour interactionBehaviour))
            {
                interactionBehaviour.RegisterTriggeredInteractable(this);
            }
            other.TryGetComponent(out grabber);
            if (grabber.ObjectToGrab is Orbe)
            {
                Debug.Log($"Il a un orbe wesh");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            HideInfos();
            if(other.TryGetComponent(out InteractionBehaviour interactionBehaviour))
            {
                interactionBehaviour.UnregisterTriggeredInteractable();
            }
            other.TryGetComponent(out grabber);
            if (grabber.ObjectToGrab is Orbe)
            {
                Debug.Log($"Il est parti avec l'orbe onion");
            }
        }
    }

    public bool Cancelable { get; set; }
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
        if (grabber.ObjectToGrab == null)
        {
            Debug.Log($"Bah mec t'as rien");
        }
        else if (grabber.ObjectToGrab is Orbe)
        {
            Debug.Log($"Bro ca fonctionne");
            if (currentSource == null)
            {
                currentSource = grabber.ObjectToGrab.GetComponentInChildren<EnergySource>();
                grabber.ObjectToGrab.EnableGrabbing();
                DropSourceSequence();
            }
        }
    }

    private async void DropSourceSequence()
    {
        await PlaceSourceSequence();
        await Task.Delay(Mathf.RoundToInt(sleepTimeBeforeActivation * 1000f));

        await EnhanceSourceSequence();
    }

    private async Task PlaceSourceSequence()
    {
        Transform sourceTransform = grabber.ObjectToGrab.transform;
        if (sourceTransform.parent != null)
        {
            sourceTransform.parent = null;
        }
        float elapsedTime = 0;
        while (elapsedTime < dropTime)
        {
            sourceTransform.position = Vector3.Lerp(sourceTransform.position, sourcePoint.position, elapsedTime / dropTime);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
        sourceTransform.position = sourcePoint.position;
    }

    private async Task EnhanceSourceSequence()
    {
        float baseRadius = currentSource.BaseSourceRadius;
        float elapsedTime = 0;
        while (elapsedTime < energyUpdateTime)
        {
            baseRadius = Mathf.Lerp(baseRadius, sourceRadius, elapsedTime / energyUpdateTime);
            currentSource.UpdateSourceRadius(baseRadius);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
    }
    
    public void CancelInteraction()
    {
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(sourcePoint.position, sourceRadius);
    }
}
