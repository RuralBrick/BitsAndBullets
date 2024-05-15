using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] TMP_Text Player1Text;
    [SerializeField] TMP_Text Player2Text;

    PlayerMovement player1;
    PlayerMovement player2;
    Dictionary<string, int> scores = new Dictionary<string, int>();

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

    public void Initialize(PlayerMovement player1, PlayerMovement player2)
    {
        this.player1 = player1;
        this.player2 = player2;
        scores.TryAdd(player1.playerName, 0);
        scores.TryAdd(player2.playerName, 0);
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        Player1Text.text = player1.playerName + ": " + scores[player1.playerName];
        Player2Text.text = player2.playerName + ": " + scores[player2.playerName];
    }

    public void AddPoint(GameObject player)
    {
        StageDesignTestScripts.PlayerBehavior playerBehavior = player.GetComponent<StageDesignTestScripts.PlayerBehavior>();

        if (playerBehavior.playerName == player1.playerName)
        {
            scores[player2.playerName] += 1;
            GameOverManager.instance.PlayerWins(player2.playerName);
        }
        else
        {
            scores[player1.playerName] += 1;
            GameOverManager.instance.PlayerWins(player1.playerName);
        }

        UpdateScoreboard();

    }

    public void AddPoint(PlayerMovement player)
    {
        scores[player.playerName] += 1;
        UpdateScoreboard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
