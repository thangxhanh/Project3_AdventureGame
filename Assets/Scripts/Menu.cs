using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    private int startMenuBuildIndex = 0;
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
    public void Restart()
    {
        SceneManager.LoadScene(startMenuBuildIndex);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
