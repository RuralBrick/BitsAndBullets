using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance { get; private set; }
    private AudioSource audioSource;

    [SerializeField] AudioClip fireSound;
    [SerializeField] AudioClip dashSound;
    [SerializeField] AudioClip powerUpCollectSound;
    [SerializeField] AudioClip gameOverSound;

    Dictionary<string, AudioClip> sounds = new Dictionary<string, AudioClip>();


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        sounds["fire"] = fireSound;
        sounds["dash"] = dashSound;
        sounds["powerupCollect"] = powerUpCollectSound;
        sounds["gameOver"] = gameOverSound;
    }

    public void PlaySound(string soundName)
    {
        audioSource.PlayOneShot(sounds[soundName]);
    }
}
