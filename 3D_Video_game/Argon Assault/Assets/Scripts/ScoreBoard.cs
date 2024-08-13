using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    int score;

    public void IncreaseScore(int amountToIncrease)
    {
        score += amountToIncrease;
        Debug.Log("Total socre: " + score); //print score for check
    }
}
