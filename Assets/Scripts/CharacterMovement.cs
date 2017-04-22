﻿// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))] // required for physics
[RequireComponent(typeof(CapsuleCollider))]
public class CharacterMovement : MonoBehaviour {
	//constants
	private const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";

	[Header("Player settings")]
	[SerializeField] private float _speed = 5f;
	[SerializeField] private float _power = 1f;
	[SerializeField] private float _rotationSpeed = 15f;
	[SerializeField] private float _jumpSpeed = 5f;
	/*[Space(5)]
	[SerializeField] private float _raiseSpeed = 5f;
	[SerializeField] private float _crouchDepth = 1f;*/

	[Header("Player input")]
	[SerializeField] private KeyCode _jumpKey = KeyCode.Space;
	//[SerializeField] private KeyCode _crouchKey = KeyCode.LeftControl;

	private CapsuleCollider _collider;
	private Rigidbody _body;
	//private float _characterHeight; //save player height
	private float _crouch;
	private float _distanceToGround = 0f;
    public float torque = 1f;

    float movementZ;

    private CharacterStateManager stateManager;

	void Start(){
		_collider = GetComponent<CapsuleCollider>();
		_body = GetComponent<Rigidbody>();
		//_characterHeight = _collider.height;

		_distanceToGround = _collider.bounds.extents.y;

		stateManager = CharacterStateManager.Instance;
	}

	void FixedUpdate() {
		float speed = _speed * Time.deltaTime, rotationSpeed = _rotationSpeed * Time.deltaTime;
		float movementX = Input.GetAxis(HORIZONTAL) * rotationSpeed;
        if (Input.GetKey(KeyCode.W)) {
		    movementZ += Time.deltaTime *  _power;
            if (movementZ > 0)
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            else if (movementZ < 0)
                movementZ = Mathf.Clamp(movementZ, -speed, 0);
        } else if (Input.GetKey(KeyCode.S)) {
		    movementZ -= Time.deltaTime *  _power/2;
            if (movementZ > 0)
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            else if (movementZ < 0)
                movementZ = Mathf.Clamp(movementZ, -speed, 0);
        }

        

		Vector3 desiredVelocity = _body.velocity;
		if(Input.GetKey(_jumpKey) && IsGrounded()){
			desiredVelocity.y = _jumpSpeed;
			_body.velocity = desiredVelocity;
		}
		if(movementZ > 0) stateManager.SetNewState(MoveState.WALK_FORWARD);
		else if(movementZ < 0) stateManager.SetNewState(MoveState.WALK_BACKWARD);
		else stateManager.SetNewState(MoveState.IDLE);

		transform.Translate(0, 0, movementZ);
        
        _body.AddRelativeTorque((Vector3.up * torque) * movementX);

		
	}

	private bool IsGrounded(){
		return Physics.Raycast(transform.position, Vector3.down, _distanceToGround + 0.1f);
	}
}
