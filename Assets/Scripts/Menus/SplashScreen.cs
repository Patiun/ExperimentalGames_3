using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SplashScreen : MonoBehaviour {

    public RawImage screen;

    public float splashTime;

    private float timeLeft;

    private bool loading = false;

	// Use this for initialization
	void Start () {
        timeLeft = splashTime;
        AudioManager.instance.Play("PeacefulSong");
	}
	
	// Update is called once per frame
	void Update () {
        timeLeft -= Time.deltaTime;


        float alpha = timeLeft / splashTime;

        screen.color = new Color(1, 1, 1, alpha);

        if(timeLeft <= 0)
        {
            if (!loading)
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                loading = true;
            }
            
        }

	}
}
