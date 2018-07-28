using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotationSpeed;

    private Rigidbody rb;
    private string xDir = "Horizontal";
    private string zDir = "Vertical";
    private string turnAxis = "Turn";

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move(Input.GetAxis(xDir),Input.GetAxis(zDir));
        Rotate(Input.GetAxis(turnAxis) * rotationSpeed);
	}

    public void Move(float x, float z)
    {
        rb.velocity = (transform.forward * z + transform.right * x).normalized * speed;
    }

    public void Rotate(float angles)
    {
        Quaternion rot = transform.rotation;
        rot.y += angles;
        transform.rotation = rot;
    }
}
