using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    PlayerManagerBehavior currentPlayerManager;
    Dictionary<string, int> scores = new Dictionary<string, int>();

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

    public void Initialize(PlayerManagerBehavior playerManager, PlayerMovement[] players)
    {
        GameInfoCanvasBehavior.Instance.Initialize(players);
        currentPlayerManager = playerManager;
        foreach (var player in players)
        {
            scores.TryAdd(player.playerName, 0);
        }
        foreach (var (playerName, score) in scores)
        {
            GameInfoCanvasBehavior.Instance.UpdateScoreboard(
                currentPlayerManager.GetPlayerNumber(playerName),
                score
            );
        }
    }

    public void AddPoint(PlayerMovement player)
    {
        scores[player.playerName] += 1;
        GameInfoCanvasBehavior.Instance.UpdateScoreboard(
            currentPlayerManager.GetPlayerNumber(player),
            scores[player.playerName]
        );
    }

    public int GetMaxScore()
    {
        return scores.Values.Max();
    }

    public string GetCurrentWinner()
    {
        int maxScore = -1;
        string maxPlayer = "";
        foreach (var score in scores)
        {
            if (score.Value > maxScore)
            {
                maxPlayer = score.Key;
            }
        }
        return maxPlayer;
    }

    public int GetNonMaxScore()
    {
        int max = GetMaxScore();
        return scores.Values.First(score => score != max);
    }

    public void StartBulletTimer(PlayerMovement player)
    {
        Debug.Log("STARTING BULLET TIMER");
        GameInfoCanvasBehavior.Instance.StartTimer(
            currentPlayerManager.GetPlayerNumber(player),
            "bullet"
        );
    }

    public void StartDashTimer(PlayerMovement player)
    {
        Debug.Log("STARTING DASH TIMER");
        GameInfoCanvasBehavior.Instance.StartTimer(
            currentPlayerManager.GetPlayerNumber(player),
            "dash"
        );
    }

    public void IncrementIcon(string icon, PlayerMovement player)
    {
        Debug.Log("Incrementing Icon");
        GameInfoCanvasBehavior.Instance.IncrementIcon(
            currentPlayerManager.GetPlayerNumber(player),
            icon);
    }

    public void RemoveIcon(string icon, PlayerMovement player)
    {
        Debug.Log("Decrementing Icon");
        GameInfoCanvasBehavior.Instance.RemoveIcon(
            currentPlayerManager.GetPlayerNumber(player),
            icon);
    }

    public void ResetIcons()
    {
        GameInfoCanvasBehavior.Instance.ResetIcons();
        Debug.Log("Icons Reset");
    }

    public void ResetScores()
    {
        scores = new Dictionary<string, int>();
    }
}
