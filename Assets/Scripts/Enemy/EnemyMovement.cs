//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed = 2f;

    private GameObject target;
    private Rigidbody body;
    private Vector3 distance;

	void Start () {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody>();
	}

    void FixedUpdate() {
        float speed = this.speed * Time.deltaTime;
        if (distance.x > 0) {
            // right
        } else if (distance.x < 0) {
            // left
        }
        transform.position = Vector3.Slerp(transform.position, target.transform.position, speed);
        transform.rotation = Quaternion.Slerp(transform.rotation, target.transform.rotation, 1);
    }

    void Update() {
        distance = transform.position - target.transform.position;
    }
}
