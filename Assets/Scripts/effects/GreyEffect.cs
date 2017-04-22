﻿//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com


using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GreyEffect : MonoBehaviour {

    public float intensity;
    private Material material;
        
    void Awake(){
        material = new Material(Shader.Find("Hidden/BWDiffuse"));
    }

    // Postprocess the image
    void OnRenderImage(RenderTexture source, RenderTexture destination) {
        if (intensity == 0) {
            Graphics.Blit(source, destination);
            return;
        }

        material.SetFloat("_bwBlend", intensity);
        Graphics.Blit(source, destination, material);
    }
}
