using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float upForce = 50f;
    [SerializeField] float rotationForce = 1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
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
            rb.AddRelativeForce(Vector3.up * upForce * Time.deltaTime, ForceMode.Force);
            //Debug.Log("Pressed Space - Thrusting");
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rotateRocket(rotationForce);
            Debug.Log("Pressed Left");
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rotateRocket(-rotationForce);

            Debug.Log("Pressed Right");
        }
    }

    void rotateRocket(float rocketRotation)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.left * rocketRotation * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
