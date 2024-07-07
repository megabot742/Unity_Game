using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    int correctAnswers = 0;
    int questionsSeen = 0;
    public int GetCorrectAnswer()
    {
        return correctAnswers;
    }
    public void IncrementCorrectAnswer()
    {
        correctAnswers++;
    }
    public int GetQuestionSeen()
    {
        return questionsSeen;
    }
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }
    public int CalculateScore()
    {
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
