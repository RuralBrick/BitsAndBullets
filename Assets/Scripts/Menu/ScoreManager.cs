using System.Collections;
using System.Collections.Generic;
using StageDesignTestScripts;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class ScoreManager : MonoBehaviour
{

    // Start is called before the first frame update
    public TMP_Text Player1Text;
    public TMP_Text Player2Text;

    Dictionary<string, int> scores = new Dictionary<string, int>();

    public static ScoreManager instance;

    string player1_name;
    string player2_name;
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
        PlayerBehavior[] players = FindObjectsOfType<PlayerBehavior>();

        player1_name = players[0].playerName;
        player2_name = players[1].playerName;

        scores.Add(player1_name, 0);
        scores.Add(player2_name, 0);

        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        Player1Text.text = player1_name + ": " + scores[player1_name];
        Player2Text.text = player2_name + ": " + scores[player2_name];
    }

    public void AddPoint(GameObject player)
    {
        PlayerBehavior playerBehavior = player.GetComponent<PlayerBehavior>();

        if (playerBehavior.playerName == player1_name)
        {
            scores[player2_name] += 1;
            GameOverManager.instance.PlayerWins(player2_name);
        }
        else
        {
            scores[player1_name] += 1;
            GameOverManager.instance.PlayerWins(player1_name);
        }

        UpdateScoreboard();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
