using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class WorldButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnClick;

    public void SetHover(bool _state)
    {
        
    }
    
    public void Click()
    {
        OnClick.Invoke();
    }
}
