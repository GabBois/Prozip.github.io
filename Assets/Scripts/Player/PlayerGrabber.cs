using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerGrabber : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    [SerializeField] private float timeToTurnToObject;
    [SerializeField] private float sleepTimeBeforeGrab;
    [SerializeField] private float timeToGrab;
    
    private Grabbable objectToGrab;
    public Grabbable ObjectToGrab => objectToGrab;
    private bool isGrabbing;

    public void GetObjectToGrab(Grabbable _obj)
    {
        objectToGrab = _obj;
    }

    public void LoseObject()
    {
        objectToGrab = null;
    }
    
    private void OnInteract(InputValue _value)
    {
        if (objectToGrab == null) return;

        if (!isGrabbing)
        {
            Grab();
        }
        else
        {
            CancelGrab();
        }
    }

    private void Grab()
    {
        TakeObjectSequence();
    }

    private async void TakeObjectSequence()
    {
        try
        {
            await TurnToObject();

            await TakeObject();
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private async Task TurnToObject()
    {
        Transform objTransform = objectToGrab.transform;
        Vector3 directionToObject = (objTransform.position - transform.position).normalized;
        directionToObject.y = 0;
        float elapsedTime = 0f;
        Quaternion targetRotation = Quaternion.LookRotation(directionToObject);

        while (elapsedTime < timeToTurnToObject)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, elapsedTime / timeToTurnToObject);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }

        transform.rotation = targetRotation;
    }

    private async Task TakeObject()
    {
        if (objectToGrab == null) return;
        
        GameEvents.TriggerInteractionStarted();
        Transform objTransform = objectToGrab.transform;
        
        objectToGrab.EnableGrabbing();
        
        float elapsedTime = 0f;
        while (elapsedTime < timeToGrab)
        {
            objTransform.position = Vector3.Lerp(objTransform.position, grabPoint.position, elapsedTime / timeToGrab);
            elapsedTime += Time.deltaTime;
            await Task.Yield();
        }
        
        objTransform.position = grabPoint.position;
        objTransform.parent = grabPoint;
        
        isGrabbing = true;
        GameEvents.TriggerInteractionEnded();
    }

    private void CancelGrab()
    {
        objectToGrab.transform.parent = null;
        objectToGrab.DisableGrabbing();
        isGrabbing = false;
    }
}
