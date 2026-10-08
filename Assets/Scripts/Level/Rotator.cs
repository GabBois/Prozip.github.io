using System;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotSpeed;

    private void Update()
    {
        transform.rotation *= Quaternion.Euler(0 ,0, rotSpeed * Time.deltaTime);
    }
}
