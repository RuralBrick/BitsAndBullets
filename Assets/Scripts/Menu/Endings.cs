using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Endings : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pointsText;

    private void Start()
    {
        string playerName = ScoreManager.instance.GetCurrentWinner();
        int winnerScore = ScoreManager.instance.GetMaxScore();
        int nonWinnerScore = ScoreManager.instance.GetNonMaxScore();
        pointsText.text = $"{playerName} wins {winnerScore} to {nonWinnerScore}!";
    }

    public void RestartButton()
    {
        ResetEverything();
        GameOverManager.instance.StartGame();
    }

    public void MainMenuButton()
    {
        ResetEverything();
        SceneManager.LoadScene("MainMenuFinal");
    }

    void ResetEverything()
    {
        TimeScaleManager.Instance.ResumeTime();
        ScoreManager.instance.ResetScores();
        GameInfoCanvasBehavior.Instance.ResetIcons();
    }
}
