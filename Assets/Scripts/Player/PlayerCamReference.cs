using System;
using UnityEngine;

public class PlayerCamReference : MonoBehaviour
{
    public static PlayerCamReference Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
}
