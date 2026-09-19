using System;
using UnityEngine;

public class PlayerCamReference : MonoBehaviour
{
    public static PlayerCamReference Instance { get; private set; }

    public Transform GetTransform => transform;

    private void Awake()
    {
        Instance = this;
    }
}
