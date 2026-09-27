using System;
using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerCamReference : MonoBehaviour
{
    public static PlayerCamReference Instance { get; private set; }
    private CinemachineCamera cam;
    [Header("Zoom")]
    [SerializeField] private float targetFOV;

    [SerializeField] private float zoomDuration;
    private float baseFOV;
    
    public Transform GetTransform => transform;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cam = GetComponent<CinemachineCamera>();
        baseFOV = cam.Lens.FieldOfView;
    }

    public void PlayZoom()
    {
        StartCoroutine(nameof(ZoomIn));
    }

    IEnumerator ZoomIn()
    {
        float elapsedTime = 0f;
        while (elapsedTime < zoomDuration)
        {
            cam.Lens.FieldOfView = Mathf.Lerp(baseFOV, 40f, elapsedTime / zoomDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        cam.Lens.FieldOfView = 50f;
    }
}
