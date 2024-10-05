using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("Elements")]
    [SerializeField] CanvasGroup menuCG;
    [SerializeField] CanvasGroup gameCG;
    [SerializeField] CanvasGroup levelCompleteCG;
    [SerializeField] CanvasGroup gameoverCG;

    [Header("Menu Elements")]
    [SerializeField] TextMeshProUGUI menuCoins;
    [SerializeField] TextMeshProUGUI menuBestScore;

    [Header("Level Complete Elements")]
    [SerializeField] TextMeshProUGUI levelCompleteCoins;
    [SerializeField] TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] TextMeshProUGUI levelCompleteScore;
    [SerializeField] TextMeshProUGUI levelCompleteBestScore;

    [Header("Gameover Elements")]
    [SerializeField] TextMeshProUGUI gameoverCoins;
    [SerializeField] TextMeshProUGUI gameoverSecretWord;
    [SerializeField] TextMeshProUGUI gameoverBestScore;

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
        ShowMenu();
        HideGame();
        HideLevelComplete();
        HideGameover();

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
            case GameState.Menu:
                ShowMenu();
                HideGame();
                break;

            case GameState.Game:
                ShowGame();
                HideMenu();
                HideLevelComplete();
                HideGameover();
                break;
                
            case GameState.LevelComplete:
                ShowLevelComplete();
                HideGame();
                break;

            case GameState.Gameover:
                ShowGameover();
                HideGame();
                break;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
    void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(menuCG);
    }
    void HideMenu()
    {
        HideCG(menuCG);
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
    void ShowGameover()
    {
        gameoverCoins.text = DataManager.instance.GetCoins().ToString();
        gameoverSecretWord.text = WordManager.instance.GetSecretWord();
        gameoverBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(gameoverCG);
    }
    void HideGameover()
    {
        HideCG(gameoverCG);
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
