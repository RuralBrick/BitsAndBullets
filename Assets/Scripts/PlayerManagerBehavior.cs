using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerBehavior : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] string playerLeftName = "Left Player";
    [SerializeField] string playerRightName = "Right Player";
    [SerializeField] Color playerLeftColor = Color.red;
    [SerializeField] Color playerRightColor = Color.blue;
    [SerializeField] Transform playerLeftSpawn;
    [SerializeField] Transform playerRightSpawn;

    public List<string> CurrentPlayers { get; private set; }

    void Start()
    {
        PlayerInput playerLeft = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: "KeyboardLeft",
            pairWithDevice: Keyboard.current
        );
        PlayerInput playerRight = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: "KeyboardRight",
            pairWithDevice: Keyboard.current
        );

        playerLeft.transform.position = playerLeftSpawn.position;
        playerRight.transform.position = playerRightSpawn.position;

        PlayerMovement playerLeftScript = playerLeft.GetComponent<PlayerMovement>();
        PlayerMovement playerRightScript = playerRight.GetComponent<PlayerMovement>();

        playerLeftScript.playerName = playerLeftName;
        playerRightScript.playerName = playerRightName;

        playerLeftScript.FaceInDirection(Vector3.right);
        playerRightScript.FaceInDirection(Vector3.left);

        playerLeft.GetComponent<SpriteRenderer>().color = playerLeftColor;
        playerRight.GetComponent<SpriteRenderer>().color = playerRightColor;

        playerLeftScript.enemy = playerRightScript;
        playerRightScript.enemy = playerLeftScript;

        CurrentPlayers = new List<string> { playerLeftName, playerRightName };

        ScoreManager.instance.Initialize(this, new PlayerMovement[] { playerLeftScript, playerRightScript });
    }

    public int GetPlayerNumber(string playerName)
    {
        return CurrentPlayers.IndexOf(playerName) + 1;
    }

    public int GetPlayerNumber(PlayerMovement player)
    {
        return CurrentPlayers.IndexOf(player.playerName) + 1;
    }
}
