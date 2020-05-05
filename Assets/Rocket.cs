using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource throttleSound;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        throttleSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Throttle(); 
        Rotate();

    }

    private void Rotate()
    {
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward);
        }
        rigidBody.freezeRotation = false;
    }

    private void Throttle()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up);
            if (!throttleSound.isPlaying)
            {
                throttleSound.Play();
            }
        }
        else
        {
            throttleSound.Stop();
        }
    }
}

    
