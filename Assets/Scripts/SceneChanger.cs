using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    int currentScene;
    void Start()
    {
         currentScene = SceneManager.GetActiveScene().buildIndex;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextScene()
    {
        SceneManager.LoadScene(currentScene+1);   
    }

    public void RestartScene()
    {
        
        SceneManager.LoadScene(currentScene);
        Time.timeScale = 1;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

}
