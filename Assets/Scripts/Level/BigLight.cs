using UnityEngine;

public class BigLight : MonoBehaviour, IReceiver
{
    [SerializeField] private Animator anim;

    private readonly int TurnOnTriggerHash = Animator.StringToHash("TurnOn");
    private readonly int TurnOffTriggerHash = Animator.StringToHash("TurnOff");
    
    public void Activate()
    {
        anim.SetTrigger(TurnOnTriggerHash);
    }

    public void Deactivate()
    {
        anim.SetTrigger(TurnOffTriggerHash);
    }
}
