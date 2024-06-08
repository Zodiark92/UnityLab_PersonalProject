using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public bool isGameWin;

    [SerializeField]
    private GameObject winScreen;

    [SerializeField]
    private GameObject gameOverScreen;

    [HideInInspector]
    public bool isGameOver;

    private GameObject player;


    private void Start()
    {
        Time.timeScale = 1f;
        player = GameObject.Find("Player");

        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);

    }

    public void GameWin()
    {
        isGameWin = true;
    }

    public void ShowVictory()
    {
        winScreen.SetActive(true);
        isGameOver = true;
    }

    public void RestartGame() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GameOver()
    {
        
        isGameOver = true;
        gameOverScreen.SetActive(true);
        Cursor.visible = true;

        Time.timeScale = 0;

    }
}
