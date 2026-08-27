using System;
using System.Collections.Generic;
using UnityEngine;

public class Theme : MonoBehaviour
{
    [SerializeField] private List<GameObject> nodeList;
    [SerializeField] private float zoneSize;

    //TODO: système de placement des nodes dans la zone 
    
    public void Initialise()
    {
        
    }

    void ReplaceNodes()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.darkGreen;
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one * zoneSize);
    }
}
