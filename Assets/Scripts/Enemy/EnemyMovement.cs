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

    public Transform target;
    private Rigidbody body;
    private Vector3 distance;
    private SphereCollider triggerSphere;

    float rotateSpeed = 10; 
    private int dIrection = -1;
    public float maxDistance = 5;
    public float directionDistance = 5;
    public float targetDistance = 5;
        float followSpeed = 11;

    float distanceValue = 1000f;
    int targetID = 0;

    void Update () {
        //Check Distance between current object and Target
        float dist = Vector3.Distance(target.position, transform.position);
        //print("Distance to other:" +dist);
        // If distance is bigger then distance between target, wander around.
        if (dist > targetDistance)  {
            //Check if there is a collider in a certain distance of the object if not then do the following
            if (!Physics.Raycast(transform.position, transform.forward, maxDistance)) {
                // Move forward
                transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
            } else {
                // If there is a object at the right side of the object then give a random direction
                if (Physics.Raycast(transform.position, transform.right, directionDistance)) {
                    dIrection = Random.Range(-1, 2);
                }
                // If there is a object at the left side of the object then give a random direction
                if (Physics.Raycast(transform.position, -transform.right, directionDistance)) {
                    dIrection = Random.Range(-1, 2);
                }
                // rotate 90 degrees in the random direction 
                transform.Rotate(Vector3.up, 90 * rotateSpeed * Time.smoothDeltaTime * dIrection);
            }
        }
        // If current distance is smaller than the given ditance, then rotate towards player, and translate the rotation into forward motion times the given speed
        if (dist> targetDistance)
     {
            transform.LookAt(target);
            transform.Translate(Vector3.forward * followSpeed * Time.smoothDeltaTime);
        }
    }
}
