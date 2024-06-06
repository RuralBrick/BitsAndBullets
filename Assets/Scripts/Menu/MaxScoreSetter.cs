using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MaxScoreSetter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ReadMaxScore();
    }

    public void ReadMaxScore()
    {
        GetComponent<TMP_InputField>().text = GameOverManager.instance.maxScore.ToString();
    }

    public void WriteMaxScore(string score)
    {
        int numScore;
        if (int.TryParse(score, out numScore))
        {
            GameOverManager.instance.maxScore = numScore;
        }
    }
}
