using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEditorInternal;
using UnityEngine;

public class EnergyRelay : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject interactTextObject;
    [SerializeField] private BoxCollider headCol;
    [SerializeField] private Transform sourcePoint;
    [SerializeField] private Focusable focusable;
    [SerializeField] private float sourceRadius;

    [Header("-[ Drop Sequence ]- ")]
    [SerializeField] private float dropTime;

    [SerializeField] private AnimationCurve dropCurve;
    [SerializeField] private float sleepTimeBeforeActivation;
    [SerializeField] private float energyUpdateTime;
    [SerializeField] private AnimationCurve energyUpdateCurve;
    
    [SerializeField] private float sleepTimeAfterActivation;

    private PlayerGrabber grabber;
    private EnergySource currentSource;
    
    private void Start()
    {
        interactTextObject.SetActive(false);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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
                StartCoroutine(nameof(DropSourceSequence));
            }
        }
    }

    private IEnumerator DropSourceSequence()
    {
        focusable.Focus();
        headCol.enabled = false;
        yield return PlaceSourceSequence();
        
        headCol.enabled = true;
        yield return new WaitForSeconds(sleepTimeBeforeActivation);
        
        yield return EnhanceSourceSequence();
        
        yield return new WaitForSeconds(sleepTimeAfterActivation);
        
        focusable.Unfocus();
    }

    private IEnumerator PlaceSourceSequence()
    {
        Transform sourceTransform = grabber.ObjectToGrab.transform;
        Vector3 sourcePos = sourceTransform.position;
        Rigidbody rb = sourceTransform.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        Debug.Log(rb.isKinematic);
        if (sourceTransform.parent != null)
        {
            sourceTransform.parent = null;
        }
        float elapsedTime = 0;
        while (elapsedTime < dropTime)
        {
            rb.isKinematic = true;
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / dropTime);
            float curved = dropCurve.Evaluate(normalizedTime);
            rb.MovePosition(Vector3.Lerp(sourcePos, sourcePoint.position, curved));
            
            yield return null;
        }
        sourceTransform.position = sourcePoint.position;
    }

    private IEnumerator EnhanceSourceSequence()
    {
        float baseRadius = currentSource.BaseSourceRadius;
        float elapsedTime = 0;
        while (elapsedTime < energyUpdateTime)
        {
            float normalizedTime = Mathf.Clamp01(elapsedTime / energyUpdateTime);
            float curved = energyUpdateCurve.Evaluate(normalizedTime);
            baseRadius = Mathf.Lerp(currentSource.BaseSourceRadius, sourceRadius, curved);
            currentSource.UpdateSourceRadius(baseRadius);
            elapsedTime += Time.deltaTime;
            yield return null;
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
