using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnnouncementText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Animator textAnim;
    [SerializeField] private float textLifeTime;
    private WaitForSeconds textLifeTimeDelay;

    Coroutine textAnimCoroutine;
    private readonly int RevealTriggerHash = Animator.StringToHash("Reveal");
    private readonly int HideTriggerHash = Animator.StringToHash("Hide");
    
    private void Start()
    {
        textLifeTimeDelay = new WaitForSeconds(textLifeTime);
        GameEvents.OnAnnouncmentTriggered += PlayText;
    }

    private void OnDisable()
    {
        GameEvents.OnAnnouncmentTriggered -= PlayText;
    }

    private void PlayText(string _content)
    {
        if (textAnimCoroutine != null)
        {
            StopCoroutine(textAnimCoroutine);
        }
        text.text = _content;
        textAnimCoroutine = StartCoroutine(nameof(TextSequence));
    }

    IEnumerator TextSequence()
    {
        textAnim.SetTrigger(RevealTriggerHash);
        yield return new WaitForSeconds(textLifeTime);
        textAnim.SetTrigger(HideTriggerHash);
    }
}
