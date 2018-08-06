using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreehouseTilt : MonoBehaviour {
    //Singleton
    public static TreehouseTilt instance;

    public float xAngle;
    public float zAngle;
    public float maxAngle;
    public Vector2 fieldSize;
    public float rateOfChange = .125f;
    public float xWind, zWind;

    private float xWeight, zWeight;
    private float xArm,zArm;
    private List<WeightedObject> objects;
    private Vector2 angles;

	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        objects = new List<WeightedObject>();
        xArm = fieldSize.x/2;
        zArm = fieldSize.y/2;
        angles = new Vector2(xAngle, zAngle);
    }
	
	// Update is called once per frame
	void Update () {
        xWeight = 0;
        zWeight = 0;

        foreach(WeightedObject wo in objects)
        {
            Vector3 displacement = wo.gameObject.transform.position - transform.position;
            xWeight += wo.weight * displacement.x / xArm;
            zWeight += wo.weight * displacement.z / zArm;
        }
        //Debug.Log(xWeight +" "+ zWeight);
        angles = Vector2.Lerp(angles, new Vector2(xWeight+xWind, zWeight+zWind),rateOfChange);
        if (angles.magnitude >= maxAngle)
        {
            Debug.Log("OH NO IT'S TILTING TOO FAR");
            GameManager.instance.WindWin();
        }

        Quaternion rotation = Quaternion.Euler(angles.y, transform.rotation.eulerAngles.y, -angles.x);
        transform.rotation = rotation;
    }

    public void OnTriggerEnter(Collider other)
    {
        WeightedObject wo = other.gameObject.GetComponent<WeightedObject>();
        if (wo != null && !objects.Contains(wo))
        {
            objects.Add(wo);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        WeightedObject wo = other.gameObject.GetComponent<WeightedObject>();
        if (wo != null && objects.Contains(wo))
        {
            objects.Remove(wo);
        }
    }

    public void Remove(WeightedObject wo)
    {
        if (wo != null && objects.Contains(wo))
        {
            objects.Remove(wo);
        }
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(new Vector3(transform.position.x,0,transform.position.z), new Vector3(fieldSize.x, 0.1f, fieldSize.y));
    }
}
