using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private int _gameScene;

    public void LoadGame()
    {
        SceneManager.LoadScene(_gameScene);
    }

}
