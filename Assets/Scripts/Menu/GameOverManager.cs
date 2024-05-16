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

    [SerializeField] int firstStageIndex = 1;
    [SerializeField] int lastStageIndex = 1;

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

    public void PlayerWins(string playerName)
    {
        gameOverText.text = "Game Over! \n" + playerName + " wins!";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void PlayerWins(PlayerMovement player)
    {
        gameOverText.text = "Game Over! \n" + player.playerName + " wins!";
        gameOverPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResetGame()
    {
        if (!gameOverPanel.activeSelf)
        {
            return;
        }
        gameOverPanel.SetActive(false); // Hide the panel
        Time.timeScale = 1;
        SceneManager.LoadScene(Random.Range(firstStageIndex, lastStageIndex + 1)); // Reset the scene
    }
}
