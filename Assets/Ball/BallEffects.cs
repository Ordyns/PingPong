using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallEffects : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem collisionParticleSystem;
    [SerializeField] private ParticleSystem deathZoneCollisionParticleSystem;

    public void OnCollision(){
        InstantiateParticle(collisionParticleSystem);
    }

    public void OnDeathCollision(){
        InstantiateParticle(deathZoneCollisionParticleSystem);
    }
    
    private void InstantiateParticle(ParticleSystem particlePrefab){
        ParticleSystem particleSystem = Instantiate(particlePrefab, transform.position, Quaternion.identity);
        Destroy(particleSystem.gameObject, particleSystem.main.duration);
    }
}
