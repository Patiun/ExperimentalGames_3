using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public static GameManager instance;

    public float timer;
    public bool playing;
    public Text SPACE;

    private float childWinTime;
    private AudioManager am;
	// Use this for initialization
	void Start () {
		if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        childWinTime = Time.time + timer;
        am = AudioManager.instance;
        am.StopAll();
        am.Play("MetalSong");
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SPACE.enabled = !SPACE.enabled;
        }
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(3);
        }
	}

    public void ChildWin()
    {

    }

    public void WindWin()
    {

    }
}
