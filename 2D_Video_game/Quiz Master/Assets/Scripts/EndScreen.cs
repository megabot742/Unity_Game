using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI finalScoreText;
    ScoreKeeper scorekeeper;
    // Start is called before the first frame update
    void Awake()
    {
        scorekeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void ShowFinalScore()
    {
        finalScoreText.text = "Congratulations!\nYou got a score: " + scorekeeper.CalculateScore() + "%"; 
    }
}
