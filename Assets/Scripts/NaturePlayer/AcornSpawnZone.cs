using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornSpawnZone : MonoBehaviour {

    public float xBound, zBound;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SpawnAcorn(GameObject prefab)
    {
        GameObject newAcorn = Instantiate(prefab);
        float x = Random.Range(-xBound, xBound) + transform.position.x;
        float z = Random.Range(-zBound, zBound) + transform.position.z;
        newAcorn.transform.parent = transform;
        newAcorn.transform.position = new Vector3(x, transform.position.y, z);
    }
}
