//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed = 2f;
    [SerializeField] private float radius = 15f;

    [SerializeField] private Transform waypointParent;

    private Transform target;
    private Rigidbody body;
    private Vector3 distance;
    private SphereCollider triggerSphere;

    float distanceValue = 1000f;
    int targetID = 0;

	void Start () {
        body = GetComponent<Rigidbody>();

        if (waypointParent == null)
            waypointParent = GameObject.FindGameObjectWithTag("Waypoints").transform;

        SearchNewPoint();
        
	}

    void FixedUpdate() {
        if (target == null) return;
        float dist = Vector3.Distance(target.position, transform.position);
        
        float speed = this.speed * Time.deltaTime;
        if (distance.x > 0) {
            // right
        } else if (distance.x < 0) {
            // left
        }
        Vector3 direction = transform.position - target.position;
        transform.position += direction * speed;
        transform.Rotate(0,10,0);

        if (dist < 2) {
            targetID++;
            if (targetID > waypointParent.childCount - 1)
                targetID = 0;
            target = waypointParent.GetChild(targetID);
        }
    }

    void SearchNewPoint () {
        for (int i = 0 ; i < waypointParent.childCount ; i++) {
            float dist = Vector3.Distance(waypointParent.GetChild(i).position, transform.position);
            if (dist < distanceValue) {
                distanceValue = dist;
                targetID = i;
            }
        }

        target = waypointParent.GetChild(targetID);
    }

    void Update() {
    }
}
