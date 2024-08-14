using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    TMP_Text scoreText;
    int score;
    void Start() 
    {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = "Start";
    }

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        scoreText.text = score.ToString();
        //Debug.Log("Total socre: " + score); //print score for check
    }
}
