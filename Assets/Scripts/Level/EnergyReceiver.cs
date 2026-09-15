using UnityEngine;
using UnityEngine.Events;

public class EnergyReceiver : MonoBehaviour
{
    [SerializeField] UnityEvent OnEnergyReceived;
    [SerializeField] UnityEvent OnEnergyLost;
    
    public void TurnOn()
    {
        OnEnergyReceived.Invoke();
    }

    public void TurnOff()
    {
        OnEnergyLost.Invoke();
    }
}
