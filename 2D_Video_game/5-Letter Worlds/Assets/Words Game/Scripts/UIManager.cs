using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Elements")]
    [SerializeField] CanvasGroup gameCG;
    [SerializeField] CanvasGroup levelCompleteCG;

    [Header("Level Complete Elements")]
    [SerializeField] TextMeshProUGUI levelCompleteCoins;
    [SerializeField] TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] TextMeshProUGUI levelCompleteScore;
    [SerializeField] TextMeshProUGUI levelCompleteBestScore;

    [Header("Game Elements")]
    [SerializeField] TextMeshProUGUI gameScore;
    [SerializeField] TextMeshProUGUI gameCoins;
    void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        ShowGame();
        HideLevelComplete();
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.Game:
                ShowGame();
                HideLevelComplete();
                break;
                
            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ShowGame()
    {
        gameCoins.text = DataManager.instance.GetCoins().ToString();
        gameScore.text = DataManager.instance.GetScore().ToString();
        ShowCG(gameCG);
    }
    void HideGame()
    {
        HideCG(gameCG);
    }
    void ShowLevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(levelCompleteCG);
    }
    void HideLevelComplete()
    {
        HideCG(levelCompleteCG);
    }

    void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }
    void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
