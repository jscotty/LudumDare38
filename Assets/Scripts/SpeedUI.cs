//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUI : MonoBehaviour {
    [SerializeField] private CharacterMovement movement;

    [SerializeField] private float maxRot= 60;

    void Update () {
        float zRot = 0;
        float percentage = movement.getMovevementZPercentage;
        if (percentage < 0) {
            percentage = -percentage;
        }
        zRot = (maxRot*2 * percentage) -maxRot;
        transform.eulerAngles = new Vector3(0,0,zRot);
    }
}
