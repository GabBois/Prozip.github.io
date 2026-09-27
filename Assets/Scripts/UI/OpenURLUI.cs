using System;
using UnityEngine;

public class OpenURLUI : MonoBehaviour
{
    [SerializeField] private Animator anim;
    
    private void OnEnable()
    {
        URLLauncher.OnURLOpened += LaunchAnimation;
    }

    private void LaunchAnimation()
    {
        anim.Play("OpenURL");
    }

    private void OnDisable()
    {
        URLLauncher.OnURLOpened -= LaunchAnimation;
    }
}
