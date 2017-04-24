//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyMovement : MonoBehaviour {

    [SerializeField] private float speed = 2f;

    [SerializeField] private Transform target;
    private Rigidbody body;
    private Vector3 distance;

    [SerializeField] private float rotateSpeed = 10; 
    private int dIrection = -1;
    [SerializeField] private float maxDistance = 10;
    [SerializeField] private float distanceToStomp = 2;
    [SerializeField] private float directionDistance = 5;
    [SerializeField] private float targetDistance = 25;
    [SerializeField] private float followSpeed = 11;


    private Vector3 targetPoint;
    private Quaternion targetRotation;

    float distanceValue = 1000f;
    int targetID = 0;
    

    void Update () {
        if (GameManager.paused) return;
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
                    dIrection = Random.Range(-2, 3);
                }
                // If there is a object at the left side of the object then give a random direction
                if (Physics.Raycast(transform.position, -transform.right, directionDistance)) {
                    dIrection = Random.Range(-2, 3);
                }
                // rotate 90 degrees in the random direction 
                transform.Rotate(Vector3.up, 90 * rotateSpeed * Time.smoothDeltaTime * dIrection);
            }
        }
        // If current distance is smaller than the given ditance, then rotate towards player, and translate the rotation into forward motion times the given speed
        if (dist< targetDistance)
     {
            targetPoint = target.transform.position - transform.position;
            targetRotation = Quaternion.LookRotation(targetPoint, transform.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);


            if (dist <= distanceToStomp) {
                GetComponent<Animator>().SetBool("stomp", true);
            } else {

                transform.Translate(Vector3.forward * speed * Time.smoothDeltaTime);
            }

        }
    }

    public void Stomped () {

        GameManager.instance.Die();
    }
}
