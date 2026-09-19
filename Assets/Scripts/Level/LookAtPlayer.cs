using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = PlayerCamReference.Instance.GetTransform;
    }

    void Update()
    {
        transform.LookAt(player);
    }
}
