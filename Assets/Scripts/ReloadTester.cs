using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadTester : MonoBehaviour
{
    bool reloaded = false;

    private void Awake()
    {
        Debug.Log("Awake called");
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start called");
        if (!reloaded)
        {
            reloaded = true;
            SceneManager.LoadScene(0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
