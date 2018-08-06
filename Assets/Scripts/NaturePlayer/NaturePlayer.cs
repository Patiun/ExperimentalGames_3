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
    public float windForce;

    public Image windImage;

    public int maxAcorns;
    public int curAcorns;
    public float acornRegenRate;
    public GameObject acornPrefab;
    public AcornSpawnZone[] dropZones;

    private TreehouseTilt treeHouseTilt;
    private readonly string verticalAxis = "Vertical_Wind";
    private readonly string horizontalAxis = "Horizontal_Wind";
    private WindRepresentation windRep;
    private bool windLockedOut;

    private float nextAcornRegen;
    private readonly float internalAcornCD = 0.5f;
    private float nextAcornAllowed;
    private readonly string acornDropButton = "Acorn1";
    private readonly string acornMultiDropButton = "Acorn2";


    // Use this for initialization
    void Start () {
        treeHouseTilt = TreehouseTilt.instance;
        curWind = maxWind;
        windRep = WindRepresentation.instance;
        curAcorns = maxAcorns;
	}
	
	// Update is called once per frame
	void Update () {
        if (canUseWind)
        {
            UseWind();
            UpdateWindImage();
        }
        if (curAcorns > 0)
        {
            if (Time.time > nextAcornAllowed)
            {
                if (Input.GetAxis(acornDropButton) > 0)
                {
                    DropAcorn();
                    RepresentAcorns();
                    nextAcornAllowed = Time.time + internalAcornCD;
                }
                if (Input.GetAxis(acornMultiDropButton) > 0 && curAcorns > 3)
                {
                    DropAcorn();
                    DropAcorn();
                    DropAcorn();
                    RepresentAcorns();
                    nextAcornAllowed = Time.time + internalAcornCD;
                }
            }
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
            if (curAcorns < maxAcorns && Time.time > nextAcornRegen)
            {
                curAcorns++;
                nextAcornRegen = Time.time + 1f / acornRegenRate;
            }
            bool horizontal = (x >= z);
            if (horizontal)
            {
                if (x > 0)
                {
                    //Debug.Log("EAST");
                    if (windRep != null)
                    {
                        windRep.Blow(1);
                        treeHouseTilt.xWind = windForce;
                    }
                }
                else
                {
                    //Debug.Log("SOUTH");
                    if (windRep != null)
                    {
                        windRep.Blow(2);
                        treeHouseTilt.zWind = windForce;
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
                        treeHouseTilt.zWind = windForce;
                    }
                }
                else
                {
                    //Debug.Log("WEST");
                    if (windRep != null)
                    {
                        windRep.Blow(3);
                        treeHouseTilt.xWind = windForce;
                    }
                }
            }
        } else
        {
            treeHouseTilt.xWind = 0;
            treeHouseTilt.zWind = 0;
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

    private void DropAcorn()
    {
        AcornSpawnZone zone = dropZones[Random.Range(0, dropZones.Length)];
        zone.SpawnAcorn(acornPrefab);
        curAcorns--;
    }

    private void RepresentAcorns()
    {

    }
}
