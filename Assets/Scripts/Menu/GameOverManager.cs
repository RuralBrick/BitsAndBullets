using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverPanel; 
    public TMP_Text gameOverText;
    public static GameOverManager instance;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // Destroy the new instance if one already exists
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverPanel.activeSelf && Input.GetKeyDown(KeyCode.Space)) // Check for a key press
        {
            ResetGame();
        }
    }

    public void PlayerWins(string playerName)
    {
        gameOverText.text = "Game Over! \n" + playerName + " wins!";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    void ResetGame()
    {
        gameOverPanel.SetActive(false); // Hide the panel
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reset the scene
    }
}
