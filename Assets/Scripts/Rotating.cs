//@Author Justin Scott Bieshaar
//Portfolio: www.justinbieshaar.com 
//Contact: contact@justinbieshaar.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotating : MonoBehaviour {

    [SerializeField] private float _speed = 1;

    void Update () {
        float speed = _speed * Time.deltaTime;
        transform.Rotate(speed, speed, speed);
    }
}
