using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManagerBehavior : MonoBehaviour
{
    [SerializeField] GameObject playerPrefab;
    [SerializeField] Transform playerLeftSpawn;
    [SerializeField] Transform playerRightSpawn;

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

        playerLeftScript.playerName = "PlayerRed";
        playerRightScript.playerName = "PlayerBlue";

        playerLeft.GetComponent<SpriteRenderer>().color = Color.red;
        playerRight.GetComponent<SpriteRenderer>().color = Color.blue;

        playerLeftScript.enemy = playerRightScript;
        playerRightScript.enemy = playerLeftScript;

        ScoreManager.instance.Initialize(playerLeftScript, playerRightScript);
    }
}
