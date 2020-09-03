using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviour
{
    //Действия кнопок в конце игры

    GameObject sceneChoice;

    private void Start()
    {
        sceneChoice = GameObject.Find("SceneChoice");        
    }

    public void LoadMenu()
    {      
        SceneManager.LoadScene(0);
    }

    public void LoadLevel()
    {       
        SceneManager.LoadScene(1);
    }
}
