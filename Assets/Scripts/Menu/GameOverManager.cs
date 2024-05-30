using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameOverManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverPanel; 
    public TMP_Text gameOverText;
    public static GameOverManager instance;

    [SerializeField] int firstStageIndex = 1;
    [SerializeField] int lastStageIndex = 1;

    int[] allStageIndices = null;
    int currentStage = 0;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);  // Destroy the new instance if one already exists
            return;
        }
        instance = this;
        allStageIndices = Enumerable.Range(firstStageIndex, lastStageIndex - firstStageIndex + 1).ToArray();
        ShuffleStageSelection();
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
        SoundEffectManager.Instance.PlaySound("gameOver");
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
        SceneManager.LoadScene(allStageIndices[currentStage++]); // Reset the scene
        if (currentStage >= allStageIndices.Length)
            ShuffleStageSelection();
    }

    void ShuffleStageSelection()
    {
        List<int> oldStageIndices = allStageIndices.ToList();
        allStageIndices = new int[allStageIndices.Length];
        currentStage = 0;
        while (currentStage < allStageIndices.Length && oldStageIndices.Count > 0)
        {
            int nextPick = Random.Range(0, oldStageIndices.Count);
            allStageIndices[currentStage++] = oldStageIndices[nextPick];
            oldStageIndices.RemoveAt(nextPick);
        }
        currentStage = 0;
    }
}
