using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Transform Enemies;
    public GameObject WinScreen;
    public GameObject loseScreen;
    public static GameManager instance;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        instance = this;
        
    }

    void Update()
    {

    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // No need for RemoveNullsFromArray method anymore

    public void CheckWin()
    {

        // Check if there are no enemies left
        if (Enemies.childCount == 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Debug.Log("You Win!");
            WinScreen.SetActive(true);
        }
    }

    public void StartGame() {    
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoseGame()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        loseScreen.SetActive(true);
    }
}
