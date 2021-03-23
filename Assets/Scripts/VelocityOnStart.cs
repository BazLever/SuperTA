using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityOnStart : MonoBehaviour
{
    public float upVelMax;
    public float forwardVelMax;
    public float rightVelMax;

    float upVel;
    float forwardVel;
    float rightVel;

    public Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        upVel = Random.Range(-upVelMax, upVelMax);
        forwardVel = Random.Range(-forwardVelMax, forwardVelMax);
        rightVel = Random.Range(-rightVelMax, rightVelMax);

        rb.AddForce((transform.forward * forwardVel) * 2);
        rb.AddForce((transform.forward * upVel) * 2);
        rb.AddForce((transform.forward * rightVel) * 2);
    }

}
