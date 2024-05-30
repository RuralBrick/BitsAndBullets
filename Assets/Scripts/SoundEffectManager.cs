using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static SoundEffectManager Instance { get; private set; }
    private AudioSource audioSource;

    public AudioClip fireSound;

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
    }

    public void PlaySound(string soundName)
    {
        Debug.Log("FIRED FROM SOUND EFFECT MANAGER");
        audioSource.PlayOneShot(sounds[soundName]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
