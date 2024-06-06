using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        // Make the pause screen inactive when we start the game
        pauseScreen.SetActive(false);

        // We do not start paused
        paused = false;
    }


    void activatePause()
    {
        Debug.Log("Pausing!");
        // Pause the game
        pauseScreen.SetActive(true);
        TimeScaleManager.Instance.StopTime();
        paused = true;
    }

    void deactivatePause()
    {
        Debug.Log("Un-Pausing!");
        // Otherwise, unpause the game
        pauseScreen.SetActive(false);
        TimeScaleManager.Instance.ResumeTime();
        paused = false;
    }

    // When we pause the game
    public void pauseGame()
    {
        // If we are not paused
        if (paused == false)
        {
            activatePause();
        }
        else
        {
            deactivatePause();
        }
    }

    public void GoToMainMenu()
    {
        SoundEffectManager.Instance.Mute();
        SceneManager.LoadScene("MainMenuFinal");
        deactivatePause();
        ScoreManager.instance.ResetScores();
    }
}
