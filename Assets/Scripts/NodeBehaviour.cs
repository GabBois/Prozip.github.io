using System;
using System.Collections;
using UnityEngine;

public class NodeBehaviour : MonoBehaviour
{
    [SerializeField] private Transform modelTransform;
    
    private Vector3 basePos;

    [Header("Animation Settings")]
    [SerializeField] private float spawnDelay = 0.1f; // Délai entre chaque node
    [SerializeField] private float scaleDuration = 0.5f; // Durée de l'animation de scale
    [SerializeField] private float moveDuration = 0.6f; // Durée de l'animation de position
    [SerializeField] private float rotationDuration = 0.8f; // Durée de l'animation de rotation
    [SerializeField] private float fadeDuration = 0.3f; // Durée de l'animation de transparence (si le matériel le supporte)
    [SerializeField] private AnimationCurve scaleCurve; // Courbe d'animation personnalisée
    [SerializeField] private AnimationCurve moveCurve; // Courbe pour le mouvement
    [SerializeField] private AnimationCurve rotationCurve; // Courbe pour la rotation
    [SerializeField] private AnimationCurve fadeCurve; // Courbe pour le fade (si matériel supporte)

    [Header("Juicy Effects")]
    [SerializeField] private bool enablePulse = true; // Effet de pulse continu
    [SerializeField] private bool enableShake = true; // Effet de shake au clic
    [SerializeField] private float pulseSpeed = 1f; // Vitesse du pulse
    [SerializeField] private float shakeMagnitude = 0.2f; // Intensité du shake
    [SerializeField] private float shakeDuration = 0.5f; // Durée du shake

    [Header("Material Settings")]
    [SerializeField] private Material material; // Matériel à animer (si tu veux changer la couleur)
    [SerializeField] private Color startColor = Color.white; // Couleur de départ
    [SerializeField] private Color endColor = new Color(0.8f, 0.8f, 1f, 1f); // Couleur cible (bleu clair)

    private Vector3 originalPosition;
    private Vector3 originalScale;
    private Quaternion originalRotation;
    private Color originalColor;
    private bool isInitialized = false;
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock propBlock; // Pour animer les propriétés du matériel
    
    private Coroutine onHoverRoutine;
    private Coroutine onExitRoutine;

    private void Awake()
    {
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        originalPosition = modelTransform.position;
        originalScale = modelTransform.localScale;
        originalRotation = modelTransform.rotation;
        originalColor = startColor;

        // Initialise le PropertyBlock pour animer le matériel
        propBlock = new MaterialPropertyBlock();
        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_Color", startColor);
        meshRenderer.SetPropertyBlock(propBlock);
    }
    
    private void Start()
    {
        basePos = modelTransform.position;
    }
    
    // Initialise le node (appelé par ThemeManager3D)
    public void InitializeNode()
    {
        if (isInitialized) return;
        isInitialized = true;

        StartCoroutine(AnimateNode());
    }

    private IEnumerator AnimateNode()
    {
        // 1. Animation de move-in (glide depuis l'arrière)
        float elapsedTime = 0f;
        
        // 2. Animation de scale (apparition)
        while (elapsedTime < scaleDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / scaleDuration;

            float curveT = scaleCurve.Evaluate(t);
            modelTransform.localScale = Vector3.Lerp(Vector3.zero, originalScale, curveT);

            // Animation de couleur (si matériel supporté)
            if (material != null)
            {
                float fadeT = fadeCurve.Evaluate(t);
                Color currentColor = Color.Lerp(startColor, endColor, fadeT);
                propBlock.SetColor("_Color", currentColor);
                meshRenderer.SetPropertyBlock(propBlock);
            }

            yield return null;
        }

        // Assure que le scale est exact
        modelTransform.localScale = originalScale;
    }

    private IEnumerator PulseEffect()
    {
        while (true)
        {
            float elapsedTime = 0f;
            Vector3 startScale = modelTransform.localScale;

            while (elapsedTime < pulseSpeed)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / pulseSpeed;

                // Animation de pulse (scale up puis down)
                float pulseScale = Mathf.PingPong(t * 2f, 1f) * 0.1f + 1f;
                modelTransform.localScale = originalScale * pulseScale;

                yield return null;
            }

            modelTransform.localScale = originalScale;
            yield return new WaitForSeconds(0.5f); // Délai entre chaque pulse
        }
    }

    // Effet de shake au clic (à appeler via EventTrigger)
    public void ShakeEffect()
    {
        Debug.Log("do shake");
        if (enableShake) StartCoroutine(ShakeRoutine());
    }

    private IEnumerator ScaleDownRoutine()
    {
        float elapsedTime = 0f;
        Vector3 startScale = modelTransform.localScale;
        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            modelTransform.localScale = Vector3.Lerp(startScale, originalScale, elapsedTime);
            yield return null;
        }
    }

    private IEnumerator ShakeRoutine()
    {
        Vector3 originalPos = modelTransform.position;
        float elapsedTime = 0f;

        while (elapsedTime < shakeDuration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;
            float z = UnityEngine.Random.Range(-1f, 1f) * shakeMagnitude;

            modelTransform.position = originalPos + new Vector3(x, y, z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        modelTransform.position = originalPos;
    }

    // Effet de hover (scale léger)
    public void OnHoverEnter()
    {
        if (onHoverRoutine != null) StopCoroutine(onHoverRoutine);
        if (onExitRoutine != null) StopCoroutine(onExitRoutine);
        onHoverRoutine = StartCoroutine(HoverScaleEffect(1.4f, 0.3f));
    }

    public void OnHoverExit()
    {
        if (onHoverRoutine != null) StopCoroutine(onHoverRoutine);
        if (onExitRoutine != null) StopCoroutine(onExitRoutine);
        onExitRoutine = StartCoroutine(HoverScaleEffect(1f, 0.3f));
    }

    private IEnumerator HoverScaleEffect(float targetScale, float duration)
    {
        float elapsedTime = 0f;
        Vector3 startScale = modelTransform.localScale;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;
            modelTransform.localScale = Vector3.Lerp(startScale, originalScale * targetScale, t);
            Debug.Log(modelTransform.localScale);
            yield return null;
        }

        modelTransform.localScale = originalScale * targetScale;
        onHoverRoutine = null;
        onExitRoutine = null;
    }


}
