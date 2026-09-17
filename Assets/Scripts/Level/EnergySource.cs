using System;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    [SerializeField] private float sourceRadius;
    [SerializeField] private SphereCollider sphereCol;
    [SerializeField] private Transform effectObject;
    private List<EnergyReceiver> energyReceiverList = new List<EnergyReceiver>();

    private float baseSourceRadius;
    public float BaseSourceRadius => baseSourceRadius;
    
    private void Start()
    {
        baseSourceRadius = sphereCol.radius;
        sphereCol.radius = sourceRadius;
        effectObject.localScale = Vector3.one * sourceRadius*2f;
    }

    public void UpdateSourceRadius(float _value)
    {
        sphereCol.radius = _value;
        effectObject.localScale = Vector3.one * _value*2f;
    }

    public void ResetSourceRadius()
    {
        sphereCol.radius = baseSourceRadius;
        effectObject.localScale = Vector3.one * baseSourceRadius*2f;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Detected {other.gameObject.name}");
        other.TryGetComponent(out EnergyReceiver receiver);
        if(!energyReceiverList.Contains(receiver))
        {
            energyReceiverList.Add(receiver);
            receiver.TurnOn();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Lost {other.gameObject.name}");
        other.TryGetComponent(out EnergyReceiver receiver);
        energyReceiverList.Remove(receiver);
        receiver.TurnOff();
    }
}
