using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    [SerializeField] int firstStageIndex = 1;
    [SerializeField] int lastStageIndex = 1;

    bool roundOver = false;
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
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        allStageIndices = Enumerable.Range(firstStageIndex, lastStageIndex - firstStageIndex + 1).ToArray();
        ShuffleStageSelection();
    }

    [System.Obsolete("Depreciated. Use PlayerWins(PlayerMovement player) instead.")]
    public void PlayerWins(string playerName) { }

    public void PlayerWins(PlayerMovement player)
    {
        SoundEffectManager.Instance.PlaySound("gameOver");
        GameInfoCanvasBehavior.Instance.ShowGameOver(player);
        TimeScaleManager.Instance.StopTime();
        roundOver = true;
    }

    public void StartGame()
    {
        SceneManager.LoadScene(allStageIndices[currentStage++]);
        if (currentStage >= allStageIndices.Length)
            ShuffleStageSelection();
    }

    public void ResetGame()
    {
        if (!roundOver) return;
        
        GameInfoCanvasBehavior.Instance.HideGameOver();
        TimeScaleManager.Instance.ResumeTime();
        SceneManager.LoadScene(allStageIndices[currentStage++]); // Reset the scene
        if (currentStage >= allStageIndices.Length)
            ShuffleStageSelection();
        roundOver = false;
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
