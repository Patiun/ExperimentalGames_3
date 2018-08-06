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
    public Text timerText;
    public Text gameOver;
    public GameObject gameOverPanel;

    private float childWinTime;
    private AudioManager am;
    private float internalTimer;

	// Use this for initialization
	void Start () {
        Time.timeScale = 1;
        if (instance != null)
        {
            Destroy(instance.gameObject);
        }
        instance = this;

        childWinTime = Time.time + timer;
        am = AudioManager.instance;
        am.StopAll();
        am.Play("MetalSong");
        internalTimer = timer;
	}
	
	// Update is called once per frame
	void Update () {
		if (playing)
        {
            internalTimer -= Time.deltaTime;
            if (internalTimer < 0) { internalTimer = 0f; }
            float minutes = Mathf.Floor(internalTimer / 60);
            float seconds = Mathf.RoundToInt(internalTimer % 60);
            string minutestxt = minutes.ToString();
            string secondstxt = seconds.ToString();

            if (minutes < 10)
            {
                minutestxt = "0" + minutes.ToString();
            }
            if (seconds < 10)
            {
                secondstxt = "0" + Mathf.RoundToInt(seconds).ToString();
            }

            timerText.text = minutestxt + ":" + secondstxt;

            if (Time.time > childWinTime) //Reset to a counter to gather the time for the UI
            {
                ChildWin();
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SPACE.enabled = !SPACE.enabled;
        }
	}

    public void ChildWin()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        Debug.Log("Child Win!");
        gameOver.text = "Child Win!";
    }

    public void WindWin()
    {
        Time.timeScale = 0;
        gameOverPanel.SetActive(true);
        Debug.Log("Nature Win!");
        gameOver.text = "Nature Win!";
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        am.StopAll();
        SceneManager.LoadScene(0);
    }
}
