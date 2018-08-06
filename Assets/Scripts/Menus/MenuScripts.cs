using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScripts : MonoBehaviour {

    public void PlayGame()
    {

        UnityEngine.SceneManagement.SceneManager.LoadScene(2);

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
