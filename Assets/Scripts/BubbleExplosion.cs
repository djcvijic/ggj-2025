using System;
using UnityEngine;

public class BubbleExplosion : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosionParticles;
    
    public void Explode()
    {
        gameObject.SetActive(true);
    }
}