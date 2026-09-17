using System;
using UnityEngine;
using Unity.Cinemachine;

public class Focusable : MonoBehaviour, IFocusable
{
    [SerializeField] private CinemachineCamera vcam;
    private int baseProrityValue;

    private void Start()
    {
        baseProrityValue = vcam.Priority;
    }

    public void Focus()
    {
        vcam.Priority = int.MaxValue;
    }

    public void Unfocus()
    {
        vcam.Priority = baseProrityValue;
    }
}
