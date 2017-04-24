//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class CarAudioController : MonoBehaviour {
    
    [SerializeField] private CharacterMovement movement;
    [SerializeField] private AudioClip[] clips = new AudioClip[3];
    private AudioSource[] audios = new AudioSource[3];

    private bool isSlipping = false;
    private bool isMoving = false;
    private bool isForward = true;

    void Start () {
        audios = GetComponents<AudioSource>();
        for (int i = 0 ; i < audios.Length ; i++) {
            audios[i].clip = clips[i];
            audios[i].loop = true;
            audios[i].playOnAwake = false;
        }

        movement.OnSlip += Skid;
        movement.OnStopSlip += StopSkid;
    }

    void Update () {
        audios[0].volume = movement.getMovevementZPercentage/2;
        audios[1].volume = -movement.getMovevementZPercentage/2;
        if (movement.movementZ > 0) {
            if (!isForward)
                isMoving = false;
            isForward = true;
            MoveAudio();
        } else if (movement.movementZ == 0) {
            StopMoveAudio();
        } else {
            if (isForward)
                isMoving = false;
            isForward = false;
            MoveBackwardsAudio();
        }
    }

    void MoveAudio () {
        if (isMoving) return;
        audios[0].Play();
        audios[1].Stop();
        isMoving = true;
    }
    void MoveBackwardsAudio () {
        if (isMoving) return;
        audios[1].Play();
        audios[0].Stop();
        isMoving = true;
    }

    void StopMoveAudio () {
        if (!isMoving) return;
        audios[0].Stop();
        audios[1].Stop();
        isMoving = false;
    }

    void Skid () {
        if (isSlipping) return;
        audios[2].Play();
        isSlipping = true;
    }
    void StopSkid () {
        if (!isSlipping) return;
        audios[2].Stop();
        isSlipping = false;
    }
}
