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

    public Animator childAnim;
    public GameObject child;

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
        bool still = rb.velocity.magnitude > (0.5f * speed);
        rb.velocity = (transform.forward * z * -1 + transform.right * x * -1).normalized * speed;
        child.transform.forward = rb.velocity.normalized * -1;

        bool change = rb.velocity.magnitude > (0.5f * speed);

        if (still && change)
        {
            childAnim.SetInteger("state", 1);


        }
        else if(!still && !change)
        {
            childAnim.SetInteger("state", 0);
            
        }
    }

    public void Rotate(float angles)
    {
        Quaternion rot = transform.rotation;
        rot.y += angles;
        transform.rotation = rot;
    }
}
