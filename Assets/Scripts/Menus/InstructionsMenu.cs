using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsMenu : MonoBehaviour {

	public void nextScene()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(3);

    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Space))
        {

            nextScene();

        }
	}
}
