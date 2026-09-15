using System;
using System.Collections.Generic;
using UnityEngine;

public class EnergySource : MonoBehaviour
{
    [SerializeField] private float sourceRadius;
    [SerializeField] private SphereCollider sphereCol;
    private List<EnergyReceiver> energyReceiverList = new List<EnergyReceiver>();

    private float baseSourceRadius;
    public float BaseSourceRadius => baseSourceRadius;
    
    private void Start()
    {
        baseSourceRadius = sphereCol.radius;
        sphereCol.radius = sourceRadius;
    }

    public void UpdateSourceRadius(float _value)
    {
        sphereCol.radius = _value;
    }

    public void ResetSourceRadius()
    {
        sphereCol.radius = baseSourceRadius;
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
