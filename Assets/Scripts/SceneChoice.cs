using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChoice : MonoBehaviour
{
    private static SceneChoice sceneChoice;

    public enum GameMode
    {
        Player, // 0 
        AI      // 1
    }

    private int choice = 0;

    public int Choice
    {
        get
        { return choice; }

        set
        {
            choice = value;
        }
    }

    private void Awake()
    {
        if (sceneChoice == null)
            sceneChoice = this;
        else
            Destroy(this.gameObject);
        DontDestroyOnLoad(sceneChoice);
    }
}
