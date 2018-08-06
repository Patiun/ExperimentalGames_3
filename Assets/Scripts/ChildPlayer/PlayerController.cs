using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed;
    public float rotationSpeed;
    public float kickForce;

    private Rigidbody rb;
    private string xDir = "Horizontal";
    private string zDir = "Vertical";
    private string turnAxis = "Turn";
    private string kick = "Kick";

    public Animator childAnim;
    public GameObject child;

    public List<GameObject> kickable;
    private TreehouseTilt treehouse;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        kickable = new List<GameObject>();
        treehouse = TreehouseTilt.instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move(Input.GetAxis(xDir),Input.GetAxis(zDir));
        Rotate(Input.GetAxis(turnAxis) * rotationSpeed);

        if(Input.GetButtonDown(kick))
        {
            childAnim.SetTrigger("kick");
            foreach (GameObject k in kickable)
            {
                Vector3 pos = k.transform.position - transform.position;
                pos.y = k.transform.position.y;
                k.GetComponent<Rigidbody>().AddForce(kickForce* (pos + 3*transform.up));
                k.GetComponent<Collider>().enabled = false;
                treehouse.Remove(k.GetComponent<WeightedObject>());
            }
        }

	}

    public void Move(float x, float z)
    {
        bool still = rb.velocity.magnitude > (0.5f * speed);
        Vector3 velocity = (transform.forward * z * -1 + transform.right * x * -1).normalized * speed;
        velocity.y = -1;
        rb.velocity = velocity;
        //child.transform.forward = rb.velocity.normalized * -1;

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

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Kickable")
        {
            kickable.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Kickable")
        {
            kickable.Remove(other.gameObject);
        }
    }
}
