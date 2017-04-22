// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))] // required for physics
public class CharacterMovement : MonoBehaviour {
	//constants
	private const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";

	[Header("Car settings")]
	[SerializeField] private float _speed = 5f;
	[SerializeField] private float _speedBackwards = 5f;
    [SerializeField] private float _power = 1f;
	[SerializeField] private float _rotationSpeed = 15f;
	[SerializeField] private float _jumpSpeed = 5f;
    
	private Rigidbody _body;

	private float _crouch;
	private float _distanceToGround = 0f;
    public float torque = 1f;

    float movementZ;

	void Start(){
		_body = GetComponent<Rigidbody>();

		_distanceToGround = 0;
	}

	void FixedUpdate() {
		float speed = _speed * Time.deltaTime, rotationSpeed = _rotationSpeed * Time.deltaTime, speedBackwards = _speedBackwards * Time.deltaTime;
        float movementX = Input.GetAxis(HORIZONTAL) * rotationSpeed;
        if (Input.GetKey(KeyCode.W)) {
		    movementZ += Time.deltaTime *  _power;
            if (movementZ > 0)
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            else if (movementZ < 0)
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
        } else if (Input.GetKey(KeyCode.S)) {
		    movementZ -= Time.deltaTime *  _power*2;
            if (movementZ > 0)
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            else if (movementZ < 0)
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
        } else {
            if (movementZ > 0) {
                movementZ = Mathf.Clamp(movementZ, 0, speed);
                movementZ -= Time.deltaTime * _power/1.001f;
            } else if (movementZ < 0) {
                movementZ += Time.deltaTime * _power/1.5f;
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
            }
        }
		transform.Translate(0, 0, movementZ);
        
        _body.AddRelativeTorque((Vector3.up * torque) * movementX);
	}

	private bool IsGrounded(){
		return Physics.Raycast(transform.position, Vector3.down, _distanceToGround + 0.1f);
	}

    private void calculatePercentage() {
        if (movementZ == 0) {
            Debug.Log(0);
            return;
        }

        if (movementZ > 0) {
            Debug.Log((movementZ/(_speed*Time.deltaTime))*100);
            return;
        }

        if (movementZ < 0) {
            Debug.Log((movementZ/ (_speedBackwards * Time.deltaTime)) *100);
            return;
        }
    }

    public float getSpeedPercentage {
        get { calculatePercentage(); return movementZ; }
    }
}
