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
    [SerializeField] Image BulletIcon1;
    [SerializeField] Image BulletIcon2;
    float bullet_cooldown = 3f;
 

    PlayerMovement player1;
    PlayerMovement player2;
    Dictionary<string, int> scores = new Dictionary<string, int>();
    Dictionary<string, (Image, float)> icons = new Dictionary<string, (Image, float)>();


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
        icons.TryAdd(player1.playerName + "_bullet_cooldown", (BulletIcon1, 0));
        icons.TryAdd(player2.playerName + "_bullet_cooldown", (BulletIcon2, 0));
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        Player1Text.text = player1.playerName + ": " + scores[player1.playerName];
        Player2Text.text = player2.playerName + ": " + scores[player2.playerName];
    }

    public void AddPoint(PlayerMovement player)
    {
        scores[player.playerName] += 1;
        UpdateScoreboard();
    }

    public void StartBulletTimer(PlayerMovement player)
    {
        Debug.Log("STARTING BULLET TIMER");
        (Image, float) cooldown_restarted = (icons[player.playerName + "_bullet_cooldown"].Item1, bullet_cooldown);
        icons[player.playerName + "_bullet_cooldown"] = cooldown_restarted;
    }

    private void updateIcons(PlayerMovement player)
    {
        float player_bullet_cooldown = icons[player.playerName + "_bullet_cooldown"].Item2;
        Image bullet_cooldown_icon = icons[player.playerName + "_bullet_cooldown"].Item1;
        Debug.Log("COOLDOWN");
        Debug.Log(player_bullet_cooldown);

        if (player_bullet_cooldown > 0)
        {
            bullet_cooldown_icon.fillAmount = 1f - (player_bullet_cooldown / bullet_cooldown);
            player_bullet_cooldown -= Time.deltaTime;
        }
        else {
            bullet_cooldown_icon.fillAmount = 1;
        }
        Debug.Log("FILL AMOUNT");
        Debug.Log(bullet_cooldown_icon.fillAmount);
        icons[player.playerName + "_bullet_cooldown"] = (bullet_cooldown_icon, player_bullet_cooldown);
    }

    // Update is called once per frame
    void Update()
    {
        updateIcons(player1);
        updateIcons(player2);
    }
}
