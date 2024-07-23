using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDeplay = 2f;
    ScoreKeeper scoreKeeper;
    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }
    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void LoadGame()
    {
        scoreKeeper.ResetScore();
        SceneManager.LoadScene("Game");
    }
    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("GameOver",sceneLoadDeplay));
    }
    public void QuitGame()
    {
        Debug.Log("Quiting game...");
        Application.Quit();
    }
    IEnumerator WaitAndLoad(string sceneName, float deplay)
    {
        yield return new WaitForSeconds(deplay);
        SceneManager.LoadScene(sceneName);
    }
}
