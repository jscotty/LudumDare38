//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour {

    [SerializeField]
    private Scrollbar musicVolume;
    [SerializeField]
    private Scrollbar effectVolume;
    private int times = 0;

    void Start () {
        times = PlayerPrefs.GetInt("times");
        if (times == 0) {
            musicVolume.value = 0.5f;
            effectVolume.value = 0.5f;
        } else {
            musicVolume.value = PlayerPrefs.GetFloat("music");
            effectVolume.value = PlayerPrefs.GetFloat("effect");
        }
        times++;
        PlayerPrefs.SetInt("times", times);

    }

    public void OnValueChanged (Scrollbar bar) {
        if (bar.Equals(musicVolume)) PlayerPrefs.SetFloat("music", bar.value);
        else if (bar.Equals(effectVolume)) PlayerPrefs.SetFloat("effect", bar.value);
    }
}
