using UnityEngine;


public class StepParticlesPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem stepParticles;
    [SerializeField] private Transform spawnPoint;
    
    public void PlayStepParticles()
    {
        stepParticles.transform.position = spawnPoint.position;
        stepParticles.Play();
    }
}
