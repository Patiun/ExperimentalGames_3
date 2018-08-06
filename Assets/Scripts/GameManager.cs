using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public float timer;
    public bool playing;

    private float childWinTime;

	// Use this for initialization
	void Start () {
		if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        childWinTime = Time.time + timer;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing)
        {
            if (Time.time > childWinTime) //Reset to a counter to gather the time for the UI
            {
                ChildWin();
            }
        }
	}

    public void ChildWin()
    {

    }

    public void WindWin()
    {

    }
}
