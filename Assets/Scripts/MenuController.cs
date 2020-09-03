using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{    
    GameObject sceneChoice;
    SceneChoice sceneChoiceScript;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Awake()
    {
        sceneChoice = GameObject.Find("SceneChoice");
        sceneChoiceScript = sceneChoice.GetComponent<SceneChoice>();
    }

    public void LoadLevelWithPlayer()
    {
        sceneChoiceScript.Choice = (int)SceneChoice.GameMode.Player;
        SceneManager.LoadScene(1);
    }

    public void LoadLevelWithAI()
    {
        sceneChoiceScript.Choice = (int)SceneChoice.GameMode.AI;
        SceneManager.LoadScene(1);
    }    

    public void ExitGame()
    {
        Application.Quit();
    }   
}
