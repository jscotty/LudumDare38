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
    public event Slip OnSlip;
    public delegate void StopSlip ();
    public event Slip OnStopSlip;

    [SerializeField] private GravityAttractor attractor;

    [Header("Car settings")]
	[SerializeField] private float speed = 5f;
	[SerializeField] private float speedBackwards = 5f;
    [SerializeField] private float power = 1f;
    [SerializeField] private float breaks = 1f;
    [SerializeField] private float rotationSpeed = 15f;
    [SerializeField] private float torque = 1f;
    
	private Rigidbody _body;

	private float _crouch;
	private float _distanceToGround = 0f;

    public float movementZ { get; private set; }
    public float movementX { get; private set; }

    private bool death = false;


    void Start(){
		_body = GetComponent<Rigidbody>();

        if (attractor == null) {
            attractor = GameObject.FindGameObjectWithTag("Attractor").GetComponent<GravityAttractor>();
        }
	}

	void Update() {
        if (GameManager.paused) {
            _body.velocity = Vector3.zero;
            return;
        }
        if (death) return;
        movementX = Input.GetAxis(HORIZONTAL) * rotationSpeed;
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.UpArrow)) {
		    movementZ +=  power;
            if (movementZ > 0)
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            else if (movementZ < 0)
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);

            if (movementZ < speed/3) {
                if (OnBurnout != null)
                    OnBurnout();
                if (OnSlip != null)
                    OnSlip();
            } else {
                if (movementZ > speed / 1.5f) {
                    if (movementX >= rotationSpeed / 2f || movementX <= -(rotationSpeed / 2f)) {
                        if (movementX >= rotationSpeed/1.1f || movementX <= -(rotationSpeed / 1.1f)) {
                            if (OnBurnout != null)
                                OnBurnout();
                            if (OnSlip != null)
                                OnSlip();
                        } else {
                            if (OnSlip != null)
                                OnSlip();
                        }
                    } else {
                        if (OnStopSlip != null)
                            OnStopSlip();
                        if (OnStopBurnout != null)
                            OnStopBurnout();
                    }
                } else {
                    if (OnStopSlip != null)
                        OnStopSlip();
                    if (OnStopBurnout != null)
                        OnStopBurnout();
                }
            }
        } else if (Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.DownArrow)) {
            if (OnSlip != null)
                OnSlip();
            if (movementZ > 0) {
                movementZ -= breaks;
                movementZ = Mathf.Clamp(movementZ, 0, speed);
            } else if (movementZ < 0) {
                movementZ -= power * 2;
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
            } else 
                movementZ -= power * 2;
        } else {
            if (movementZ > 0) {
                movementZ = Mathf.Clamp(movementZ, 0, speed);
                movementZ -= power/1.001f;
            } else if (movementZ < 0) {
                movementZ += power/1.5f;
                movementZ = Mathf.Clamp(movementZ, -speedBackwards, 0);
            }
            if (OnStopBurnout != null)
                OnStopBurnout();



            if (movementZ > speed / 2) {
                if (movementX >= rotationSpeed / 2f || movementX <= -(rotationSpeed / 2f)) {
                    if (movementX >= rotationSpeed / 1.3f || movementX <= -(rotationSpeed / 1.3f)) {
                        if (OnBurnout != null)
                            OnBurnout();
                        if (OnSlip != null)
                            OnSlip();
                    } else {
                        if (OnSlip != null)
                            OnSlip();
                    }
                } else {
                    if (OnStopSlip != null)
                        OnStopSlip();
                    if (OnStopBurnout != null)
                        OnStopBurnout();
                }
            } else {
                if (OnStopSlip != null)
                    OnStopSlip();
                if (OnStopBurnout != null)
                    OnStopBurnout();
            }
        }

        if (movementZ!=0)
            transform.Rotate(0, movementX*Time.deltaTime, 0);

        Vector3 direction = new Vector3(transform.forward.x * movementZ, transform.forward.y * movementZ, transform.forward.z * movementZ);
        _body.velocity = direction + attractor.Gravity(transform);
    }

    public float getMovevementZPercentage {
        get { return movementZ / speed; }
    }

    void OnCollisionEnter (Collision collision) {
        if(collision.gameObject.tag != "Attractor")
            movementZ = 0;
    }

    public void Die () {
        death = true;
        movementZ = 0;
        _body.velocity = Vector3.zero;
    }

}
