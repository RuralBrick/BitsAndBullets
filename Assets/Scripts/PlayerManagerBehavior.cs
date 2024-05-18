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

        playerLeft.GetComponent<SpriteRenderer>().color = playerLeftColor;
        playerRight.GetComponent<SpriteRenderer>().color = playerRightColor;

        playerLeftScript.enemy = playerRightScript;
        playerRightScript.enemy = playerLeftScript;

        ScoreManager.instance.Initialize(playerLeftScript, playerRightScript);
    }
}
