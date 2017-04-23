//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleBehaviour : MonoBehaviour {
    
    [SerializeField] private CharacterMovement movement;
    private ParticleSystem particles;

	void Start () {
        particles = GetComponent<ParticleSystem>();
        movement.OnBurnout += StartParticle;
        movement.OnStopBurnout += StopParticle;
        particles.Stop();
    }

    void StartParticle () {
        particles.Play();
    }

    void StopParticle () {
        particles.Stop();
    }
}
