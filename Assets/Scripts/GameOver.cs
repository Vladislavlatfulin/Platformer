using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void RestartHandler()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1f;
    }

    public void ExitHandler()
    {
        SceneManager.LoadScene("Menu Scene");
        Time.timeScale = 1f;
    }

}
