using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager instance;

    public int maxScore = 10;
    [SerializeField] int firstStageIndex = 1;
    [SerializeField] int lastStageIndex = 1;

    bool[] stageMask = null;
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
        transform.SetParent(null, worldPositionStays: true);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        stageMask = new bool[lastStageIndex - firstStageIndex + 1];
        for (int i = 0; i < stageMask.Length; i++)
        {
            stageMask[i] = true;
        }
        allStageIndices = Enumerable.Range(firstStageIndex, lastStageIndex - firstStageIndex + 1)
                                    .Where(index => stageMask[index - firstStageIndex])
                                    .ToArray();
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

    public void ReturnToMainMenu()
    {
        SoundEffectManager.Instance?.Mute();
        SceneManager.LoadScene("MainMenuFinal");
        allStageIndices = Enumerable.Range(firstStageIndex, lastStageIndex - firstStageIndex + 1)
                                    .Where(index => stageMask[index - firstStageIndex])
                                    .ToArray();
        ShuffleStageSelection();
    }

    public void StartGame()
    {
        if (currentStage >= allStageIndices.Length)
            ShuffleStageSelection();
        SceneManager.LoadScene(allStageIndices[currentStage++]);
        SoundEffectManager.Instance?.Unmute();
    }

    public void ResetGame()
    {
        if (!roundOver) return;
        
        GameInfoCanvasBehavior.Instance.HideGameOver();
        TimeScaleManager.Instance.ResumeTime();
        roundOver = false;

        if (ScoreManager.instance.GetMaxScore() >= maxScore)
        {
            SceneManager.LoadScene("Ending");
        }
        else
        {
            if (currentStage >= allStageIndices.Length)
                ShuffleStageSelection();
            SceneManager.LoadScene(allStageIndices[currentStage++]); // Reset the scene
        }
    }

    public bool GetLevelSelection(int index)
    {
        return stageMask[index];
    }

    public void SetLevel(int index, bool selection)
    {
        stageMask[index] = selection;
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
