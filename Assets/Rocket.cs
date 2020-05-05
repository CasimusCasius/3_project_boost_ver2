using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;
    [SerializeField] float throttleSpeed = 100f;

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

    void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("OK");
                break;
            default:
                print("Umarłeś");
                break;
        }
    }

    private void Rotate()
    {
        float rotationPerFrame = rotationSpeed * Time.deltaTime;
        rigidBody.freezeRotation = true;
        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationPerFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward*rotationPerFrame);
        }
        rigidBody.freezeRotation = false;
    }

    private void Throttle()
    {
        float throttlePerFrame = throttleSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            rigidBody.AddRelativeForce(Vector3.up*throttlePerFrame);
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

    
