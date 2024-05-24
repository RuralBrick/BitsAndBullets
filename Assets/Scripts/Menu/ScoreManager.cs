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

    [SerializeField] Image Player1Bar;
    [SerializeField] Image Player2Bar;
    [SerializeField] TMP_Text Player1Text;
    [SerializeField] TMP_Text Player2Text;
    [SerializeField] Image BulletIcon1;
    [SerializeField] Image BulletIcon2;
    [SerializeField] Image DashIcon1;
    [SerializeField] Image DashIcon2;

    Dictionary<string, float> cooldowns = new Dictionary<string, float>
    {
        { "_bullet_cooldown", 3f },
        { "_dash_cooldown", 3f }
    };

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
        icons.TryAdd(player1.playerName + "_dash_cooldown", (DashIcon1, 0));
        icons.TryAdd(player2.playerName + "_dash_cooldown", (DashIcon2, 0));
        UpdateScoreboard();
    }

    private void UpdateScoreboard()
    {
        Player1Bar.color = player1.GetComponent<SpriteRenderer>().color;
        Player2Bar.color = player2.GetComponent<SpriteRenderer>().color;
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
        (Image, float) cooldown_restarted = (icons[player.playerName + "_bullet_cooldown"].Item1, cooldowns["_bullet_cooldown"]);
        icons[player.playerName + "_bullet_cooldown"] = cooldown_restarted;
    }

    public void StartDashTimer(PlayerMovement player)
    {
        Debug.Log("STARTING DASH TIMER");
        (Image, float) cooldown_restarted = (icons[player.playerName + "_dash_cooldown"].Item1, cooldowns["_dash_cooldown"]);
        icons[player.playerName + "_dash_cooldown"] = cooldown_restarted;
    }

    public void ResetIcons()
    {
        List<string> keys = new List<string>(icons.Keys);
        foreach (string icon_name in keys)
        {
            Image icon = icons[icon_name].Item1;
            icons[icon_name] = (icon, 0);
        }
        Debug.Log("Icons Reset");
    }

    private void updateIcons(PlayerMovement player)
    {
        foreach (string icon_type in cooldowns.Keys)
        {
            float current_cooldown = icons[player.playerName + icon_type].Item2;
            Image icon = icons[player.playerName + icon_type].Item1;
            SpriteRenderer playerSprite = player.GetComponent<SpriteRenderer>();
            icon.color = playerSprite.color;

            if (current_cooldown > 0)
            {
                icon.fillAmount = 1f - (current_cooldown / cooldowns[icon_type]);
                current_cooldown -= Time.deltaTime;
            }
            else
            {
                icon.fillAmount = 1;
            }

            icons[player.playerName + icon_type] = (icon, current_cooldown);
        }

    }

    // Update is called once per frame
    void Update()
    {
        updateIcons(player1);
        updateIcons(player2);
    }
}
