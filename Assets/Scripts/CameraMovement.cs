// @author: Justin Scott Bieshaar
// please contact me at contact@justinbieshaar.com if you have any issues!
// or read the comments ;)

using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private const string MOUSE_X = "Mouse X", MOUSE_Y = "Mouse Y";

    public GameObject target;
    public float rotateSpeed = 5;
    Vector3 offset;

    void Start () {
        offset = target.transform.position - transform.position;
    }

    void Update () {
        if (!Input.GetMouseButton(0)) return;
        transform.RotateAround(transform.parent.position, transform.parent.up, Input.GetAxis(MOUSE_X) * (rotateSpeed * Time.deltaTime));
    }
}
