using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    //enemy list
    public List<GameObject> Enemies;

    public bool isGameActive;
    private int score;
    //text components
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI ammoText;

    public float lives = 1;
    //TITLE SCREEN
    public GameObject titleScreen;
    public GameObject pauseScreen;
    private bool paused;

    public Button startButton;

    public Button restartButton;

    
    // Start is called before the first frame update\
    void Start()
    {
        Button btn = startButton.GetComponent<Button>();
        btn.onClick.AddListener(StartGame);
        
    }

    public void StartGame()
    {
        
        isGameActive = true;
        score = 0;
        Time.timeScale = 1; // resume all physical calculations
        StartCoroutine(SpawnEnemy());
        UpdateScore(0);
        UpdateLives(0);
        titleScreen.gameObject.SetActive(false);
        //hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            //PauseGame();
            ChangePaused();
        }
    }

   

    IEnumerator SpawnEnemy()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(2);
            int index = Random.Range(0, Enemies.Count);
            Instantiate(Enemies[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateLives(float livesToChange)
    {
        lives += livesToChange;
        livesText.text = "Lives: " + lives;

        if(lives <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameActive = false;

        //unlock the cursor
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0; //stops physical calculatios
        Debug.Log("GAME OVER");
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
