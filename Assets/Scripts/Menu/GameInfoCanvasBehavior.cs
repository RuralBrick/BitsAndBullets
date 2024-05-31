using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class IconGroup
{
    public string name;
    public float totalCooldown;
    public IconBehavior[] icons;
}

public class GameInfoCanvasBehavior : MonoBehaviour
{
    public static GameInfoCanvasBehavior Instance { get; private set; }

    [SerializeField] Image[] PlayerBars = new Image[2];
    [SerializeField] TMP_Text[] PlayerText = new TMP_Text[2];

    [SerializeField] IconGroup[] IconInfo = new IconGroup[]
    {
        new IconGroup
        {
            name = "bullet",
            totalCooldown = 3f,
            icons = new IconBehavior[2]
        },
        new IconGroup
        {
            name = "dash",
            totalCooldown = 3f,
            icons = new IconBehavior[2]
        }
    };

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] TMP_Text gameOverText;

    string[] playerNames;
    Dictionary<string, IconBehavior[]> icons = new Dictionary<string, IconBehavior[]>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            transform.SetParent(null, worldPositionStays: true);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        foreach (var group in IconInfo)
        {
            icons[group.name] = group.icons;
        }
    }

    public void Initialize(PlayerMovement[] players)
    {
        foreach (var group in IconInfo)
        {
            for (int i = 0; i < players.Length; i++)
            {
                group.icons[i].Initialize(players[i], group.totalCooldown);
            }
        }

        playerNames = new string[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            PlayerBars[i].color = players[i].GetComponent<SpriteRenderer>().color;
            playerNames[i] = players[i].playerName;
        }
    }

    public void UpdateScoreboard(int playerNumber, int score)
    {
        PlayerText[playerNumber - 1].text = playerNames[playerNumber - 1] + ": " + score;
    }

    public void StartTimer(int playerNumber, string iconName)
    {
        icons[iconName][playerNumber - 1].StartCooldown();
    }

    public void ResetIcons()
    {
        foreach (var group in IconInfo)
        {
            foreach (var icon in group.icons)
            {
                icon.Reset();
            }
        }
    }

    public void ShowGameOver(PlayerMovement player)
    {
        gameOverText.text = "Game Over! \n" + player.playerName + " wins!";
        gameOverPanel.SetActive(true);
    }

    public void HideGameOver()
    {
        gameOverPanel.SetActive(false); // Hide the panel
    }
}
