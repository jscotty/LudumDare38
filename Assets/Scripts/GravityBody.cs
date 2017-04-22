//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

    private Rigidbody body;
    [SerializeField] private GravityAttractor attractor;

	void Start () {
        if (attractor == null) {
            attractor = GameObject.FindGameObjectWithTag("Attractor").GetComponent<GravityAttractor>();
        }
        body = GetComponent<Rigidbody>();
        body.useGravity = false;
	}

    void Update() {
        attractor.Attract(transform, body);
    }
}
