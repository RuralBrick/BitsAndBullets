using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace StageDesignTestScripts
{
    public class PlayerManagerBehavior : MonoBehaviour
    {
        [SerializeField] GameObject playerPrefab;
        [SerializeField] Transform playerLeftSpawn;
        [SerializeField] Transform playerRightSpawn;

        // Start is called before the first frame update
        void Start()
        {
            PlayerInput playerLeft = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "KeyboardLeft",
                pairWithDevice: Keyboard.current
            );
            playerLeft.transform.position = playerLeftSpawn.position;
            playerLeft.GetComponent<SpriteRenderer>().color = Color.red;
            playerLeft.GetComponent<PlayerBehavior>().playerName = "PlayerRed";
            PlayerInput playerRight = PlayerInput.Instantiate(
                playerPrefab,
                controlScheme: "KeyboardRight",
                pairWithDevice: Keyboard.current
            );
            playerRight.transform.position = playerRightSpawn.position;
            playerRight.GetComponent<SpriteRenderer>().color = Color.blue;
            playerRight.GetComponent<PlayerBehavior>().playerName = "PlayerBlue";
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
