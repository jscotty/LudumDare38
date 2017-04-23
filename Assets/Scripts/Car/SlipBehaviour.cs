//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipBehaviour : MonoBehaviour {
    
	[SerializeField] private CharacterMovement movement;
	[SerializeField] private GameObject slip;

    private bool isSlipping = false;
    private GameObject slipObject;
    private bool isBurnout = false;

	void Start () {
        movement.OnSlip += OnSlip;
        movement.OnStopSlip += OnStopSlip;
    }

    void OnBurnout () {
        isBurnout = true;
        OnSlip();
    }
    void OnStopBurnout () {
        if (!isBurnout) return;
        isBurnout = false;
        OnStopSlip();
    }

    void OnSlip () {
        if (!isSlipping) {
            isSlipping = true;
            slipObject = (GameObject) Instantiate(slip, transform.position, transform.rotation);
        }

        if (isSlipping) {
            slipObject.transform.position = transform.position;
        }
    }

    void OnStopSlip () {
        if (isBurnout) return;
        isSlipping = false;
    }
}
