using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Transform target;

    [Header("Settings")] [SerializeField] private Vector3 offset;
    [SerializeField] private float moveSpeed;

    private void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, target.position - offset, Time.deltaTime * moveSpeed);
    }
}
