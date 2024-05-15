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
        playerLeft.transform.position = playerLeftSpawn.position;
        playerLeft.GetComponent<SpriteRenderer>().color = Color.red;
        playerLeft.GetComponent<PlayerMovement>().playerName = "PlayerRed";
        PlayerInput playerRight = PlayerInput.Instantiate(
            playerPrefab,
            controlScheme: "KeyboardRight",
            pairWithDevice: Keyboard.current
        );
        playerRight.transform.position = playerRightSpawn.position;
        playerRight.GetComponent<SpriteRenderer>().color = Color.blue;
        playerRight.GetComponent<PlayerMovement>().playerName = "PlayerBlue";
        ScoreManager.instance.Initialize(
            playerLeft.GetComponent<PlayerMovement>(),
            playerRight.GetComponent<PlayerMovement>()
        );
    }
}
