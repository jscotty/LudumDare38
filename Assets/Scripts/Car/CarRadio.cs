//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class CarRadio : MonoBehaviour {

    [SerializeField] private Radio[] channels;
    [SerializeField] private Text radioAudioOutput;
    [SerializeField] private Text radioChannelOutput;
    private AudioSource source;
    private int activeChannel = 0;
    private int activeAudio = 0;

    void Start () {
        source = GetComponent<AudioSource>();
        PlaySound();
    }

    void Update () {
        if (!source.isPlaying) Next();
        for (int i = 0 ; i < channels.Length ; i++) {
            if (Input.GetKeyUp(KeyCode.Alpha1+i)) {
                activeChannel = i;
                activeAudio = 0;
                PlaySound();
            }

        }

        if (Input.GetKeyUp(KeyCode.Space)) {
            Next();
        }
    }

    void PlaySound () {
        source.Stop();
        source.clip = channels[activeChannel].tracks[activeAudio];
        source.Play();
        SetAudioOutput();
    }

    void Next () {
        activeAudio++;
        if (activeAudio > channels[activeChannel].tracks.Length - 1)
            activeAudio = 0;
        PlaySound();
    }

    void SetAudioOutput () {
        Debug.Log(source.clip.name);
        radioAudioOutput.text =  source.clip.name;
        radioChannelOutput.text = "Channel: " + channels[activeChannel].channelName;
    }
}

[System.Serializable]
public struct Radio {
    public AudioClip[] tracks;
    public string channelName;
}
