using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaturePlayer : MonoBehaviour {

    public bool canUseWind;
    public float maxWind;
    public float curWind;
    public float regenRate;
    public float drainRate;

    public Image windImage;

    private TreehouseTilt treeHouseTilt;
    private readonly string verticalAxis = "Vertical";
    private readonly string horizontalAxis = "Horizontal";
    private WindRepresentation windRep;
    private bool windLockedOut;

	// Use this for initialization
	void Start () {
        treeHouseTilt = TreehouseTilt.instance;
        curWind = maxWind;
        windRep = WindRepresentation.instance;
	}
	
	// Update is called once per frame
	void Update () {
        if (canUseWind)
        {
            UseWind();
            UpdateWindImage();
        }
	}

    private void UseWind()
    {
        if (curWind < 0)
        {
            curWind = 0f;
            windLockedOut = true;
        }
        float x = Input.GetAxis(horizontalAxis);
        float z = Input.GetAxis(verticalAxis);
        if (!windLockedOut && (x != 0 || z != 0))
        {
            curWind -= drainRate;
            bool horizontal = (x >= z);
            if (horizontal)
            {
                if (x > 0)
                {
                    //Debug.Log("EAST");
                    if (windRep != null)
                    {
                        windRep.Blow(1);
                    }
                }
                else
                {
                    //Debug.Log("SOUTH");
                    if (windRep != null)
                    {
                        windRep.Blow(2);
                    }
                }
            } else
            {
                if (z > 0)
                {
                    //Debug.Log("NORTH");
                    if (windRep != null)
                    {
                        windRep.Blow(0);
                    }
                }
                else
                {
                    //Debug.Log("WEST");
                    if (windRep != null)
                    {
                        windRep.Blow(3);
                    }
                }
            }
        } else
        {
            curWind += regenRate;
            if (curWind >= maxWind)
            {
                windLockedOut = false;
                curWind = maxWind;
            }
            if (windRep != null)
            {
                windRep.StopBlowing();
            }
        }
    }

    private void UpdateWindImage()
    {
        if (windImage != null)
        {
            windImage.fillAmount = (curWind / maxWind);
        }
    }
}
