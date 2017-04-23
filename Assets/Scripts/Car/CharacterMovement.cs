// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))] // required for physics
public class CharacterMovement : MonoBehaviour {
	//constants
	private const string HORIZONTAL = "Horizontal", VERTICAL = "Vertical";

    public delegate void Burnout ();
    public event Burnout OnBurnout;
    public delegate void StopBurnout ();
    public event StopBurnout OnStopBurnout;
    public delegate void Slip ();

	[Header("Car settings")]
	[SerializeField] private float _speed = 5f;
	[SerializeField] private float _speedBackwards = 5f;
    [SerializeField] private float _power = 1f;
    [SerializeField] private float _breaks = 1f;
    [SerializeField] private float _rotationSpeed = 15f;
    [SerializeField] private float torque = 1f;
    
	private Rigidbody _body;

	private float _crouch;
	private float _distanceToGround = 0f;

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

            if (movementZ < 0.1f) {
                if (OnBurnout != null)
                    OnBurnout();
            } else {
                if (OnStopBurnout != null)
                    OnStopBurnout();
            }
        } else if (Input.GetKey(KeyCode.S)) {
            if (movementZ > 0) {
                movementZ -= Time.deltaTime * _breaks;
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            } else if (movementZ < 0) {
                movementZ -= Time.deltaTime * _power * 2;
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
            } else 
                movementZ -= Time.deltaTime * _power * 2;
        } else {
            if (movementZ > 0) {
                movementZ = Mathf.Clamp(movementZ, 0, speed);
                movementZ -= Time.deltaTime * _power/1.001f;
            } else if (movementZ < 0) {
                movementZ += Time.deltaTime * _power/1.5f;
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
            }
            if (OnStopBurnout != null)
                OnStopBurnout();
        }
		transform.Translate(0, 0, movementZ);

        if (movementZ!=0)
            transform.Rotate(0, movementX, 0);
    }

	private bool IsGrounded(){
		return Physics.Raycast(transform.position, Vector3.down, _distanceToGround + 0.1f);
	}
}
