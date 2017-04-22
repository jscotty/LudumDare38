//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour {

    [SerializeField]
    private float gravity = -10.0f;

	void Awake () {
        transform.tag = "Attractor";
	}

    public void Attract(Transform target, Rigidbody body) {
        Vector3 gravityUp = (target.position - transform.position).normalized;
        Vector3 targetUp = target.up;

        body.AddForce(gravityUp * gravity);
        Quaternion targetRotation = Quaternion.FromToRotation(targetUp, gravityUp) * target.rotation;
        target.rotation = Quaternion.Slerp(target.rotation, targetRotation, 50 * Time.deltaTime);
    }

    public void RotateToAttractor(Transform target) {

        Vector3 gravityUp = (target.position - transform.position).normalized;
        Vector3 targetUp = target.up;
        Quaternion targetRotation = Quaternion.FromToRotation(targetUp, gravityUp) * target.rotation;
        target.rotation = targetRotation;
    }
}
