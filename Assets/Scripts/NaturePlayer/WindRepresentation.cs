using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindRepresentation : MonoBehaviour {
    public static WindRepresentation instance;

    private Material windMat;
    private Vector3 angles;
    private readonly string slowSpeed = "Vector1_C8D65CF5";
    private readonly string fastSpeed = "Vector1_6E3BFA18";

	// Use this for initialization
	void Start () {
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;
        windMat = GetComponent<MeshRenderer>().material;
        windMat.SetFloat(fastSpeed, 0.02f);
        windMat.SetFloat(slowSpeed, 0.02f);
        angles = new Vector3();
    }
	
	public void StopBlowing()
    {
        windMat.SetFloat(fastSpeed, 0.02f);
        windMat.SetFloat(slowSpeed, 0.02f);
        Vector3 targetAngle = new Vector3(0, 0, 0);
        angles = Vector3.Lerp(angles, targetAngle, 0.25f);
        transform.rotation = Quaternion.Euler(angles);
    }

    public void Blow(int dir)
    {
        windMat.SetFloat(fastSpeed, 0.22f);
        windMat.SetFloat(slowSpeed, 0.14f);
        float angle = 0;
        switch (dir)
        {
            case 0:
                //North;
                angle = -90f;
                break;
            case 1:
                //East
                angle = 0f;
                break;
            case 2:
                //South
                angle = 90f;
                break;
            case 3:
                //West
                angle = 180f;
                break;
        }
        Vector3 targetAngle = new Vector3(0, angle, 0);
        angles = Vector3.Lerp(angles, targetAngle, 0.25f);
        transform.rotation = Quaternion.Euler(angles);
    }
}
