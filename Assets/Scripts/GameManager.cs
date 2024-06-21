using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public bool isGameWin;

    [SerializeField]
    private GameObject winScreen;

    [HideInInspector]
    public bool isGameStarted;

    [SerializeField]
    private GameObject gameOverScreen;

    [SerializeField]
    private GameObject mainMenu;

    [SerializeField]
    private GameObject UIScreen;

    [SerializeField]
    private TextMeshProUGUI booksText;

    private int totalBooks;
    private int booksFound;

    [HideInInspector]
    public bool isGameOver;

    private GameObject player;


    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.visible = true;

        isGameOver = false;
        isGameStarted = false;
    }

    private void Start()
    {
       
        player = GameObject.Find("Player");
        totalBooks = GameObject.Find("SpawnManager").GetComponent<SpawnManager>().initialItemCount;
        booksFound = 0;
        booksText.text = booksFound + "/" + totalBooks;
       
        winScreen.SetActive(false);
        gameOverScreen.SetActive(false);
        mainMenu.SetActive(true);
        UIScreen.SetActive(false);

    }

    public void GameWin()
    {
        isGameWin = true;
    }

    public void ShowVictory()
    {
        winScreen.SetActive(true);
        Cursor.visible = true;
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

    public void GameStart()
    {
        isGameStarted = true;
        Cursor.visible = false;

        mainMenu.SetActive(false);
        UIScreen.SetActive(true);
        
    }

    public void UpdateBooksFound()
    {
        booksFound++;
        booksText.text = booksFound + "/" + totalBooks;
    }
}
