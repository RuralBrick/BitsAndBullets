using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    // Set up the varaibles that we need
    // Pass the pause screen
    [SerializeField] GameObject pauseScreen;

    // pause bool
    bool paused = false;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
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
        // Make the pause screen inactive when we start the game
        pauseScreen.SetActive(false);

        // We do not start paused
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // When we pause the game
    public void pauseGame()
    {
        // If we are not paused
        if (paused == false)
        {
            Debug.Log("Pausing!");
            // Pause the game
            pauseScreen.SetActive(true);
            TimeScaleManager.Instance.StopTime();
            paused = true;
        }
        else
        {
            Debug.Log("Un-Pausing!");
            // Otherwise, unpause the game
            pauseScreen.SetActive(false);
            TimeScaleManager.Instance.ResumeTime();
            paused = false;
        }
    }


}
