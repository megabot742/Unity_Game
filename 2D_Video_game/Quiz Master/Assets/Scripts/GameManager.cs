using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    Quiz quiz;
    EndScreen endscreen;
    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endscreen = FindObjectOfType<EndScreen>();
    }
    void Start()
    {
        quiz.gameObject.SetActive(true);
        endscreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(quiz.IsComplete)
        {
            quiz.gameObject.SetActive(false);
            endscreen.gameObject.SetActive(true);
            endscreen.ShowFinalScore();
        }
    }
    public void OnReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
