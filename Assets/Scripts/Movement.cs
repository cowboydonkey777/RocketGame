using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{

    [SerializeField] float upForce = 50f;
    [SerializeField] float rotationForce = 1f;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;

    Rigidbody rb;
    AudioSource rocketAudio;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rocketAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime, ForceMode.Force);
        if (!rocketAudio.isPlaying)
        {
            rocketAudio.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    void StopThrusting()
    {
        rocketAudio.Stop();
        mainEngineParticles.Stop();
    }

    void RotateLeft()
    {
        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
        rotateRocket(rotationForce);
    }

    void RotateRight()
    {
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
        rotateRocket(-rotationForce);
    }

    void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    void rotateRocket(float rocketRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.left * rocketRotation * Time.deltaTime);
        rb.constraints = RigidbodyConstraints.FreezeRotationY |
        RigidbodyConstraints.FreezeRotationZ |
        RigidbodyConstraints.FreezePositionX;
    }
}
