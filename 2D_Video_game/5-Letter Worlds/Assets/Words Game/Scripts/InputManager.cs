using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [Header("Elements")]
    [SerializeField] WordContainer[] wordContainers;
    [SerializeField] Button tryButton;
    [SerializeField] KeyboardColorizer keyboardColorizer;

    [Header("Setting")]
    int currentWordContainerIndex;
    bool canAddLetter = true;
    bool shouldReset; // false

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    void OnDestroy() 
    {
        KeyboardKey.onKeyPressed -= KeyPressedCallback;
        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    void GameStateChangedCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.Game:
                if(shouldReset)
                    Initialize();
                break;
            
            case GameState.LevelComplete:
                shouldReset =  true;
                break;
            
            case GameState.Gameover:
                shouldReset = true;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Initialize()
    {
        currentWordContainerIndex = 0;
        canAddLetter = true;

        DisableTryButton();

        for(int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
        shouldReset = false;
    }
    void KeyPressedCallback(char letter)
    {
        if(!canAddLetter)
            return;

        wordContainers[currentWordContainerIndex].Add(letter);

        if(wordContainers[currentWordContainerIndex].IsComplete())
        {
            canAddLetter = false;
            EnableTryButton();
            //CheckWord();
        }
        
    }
    public void CheckWord()
    {
        string wordToCheck = wordContainers[currentWordContainerIndex].GetWord();
        string secretWord = WordManager.instance.GetSecretWord();

        wordContainers[currentWordContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);


        if(wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else
        {
            //Debug.Log("Wrong word");
            currentWordContainerIndex++;
            DisableTryButton();

            if(currentWordContainerIndex >= wordContainers.Length)
            {
               //Debug.Log("Gameover");
               DataManager.instance.ResetScore();
               GameManager.instance.SetGameState(GameState.Gameover);
            }
            else
            {
                canAddLetter = true;
            }
        }
    }
    void SetLevelComplete()
    {
        UpdateData();
        GameManager.instance.SetGameState(GameState.LevelComplete);
    }
    void UpdateData()
    {
        int scoreToAdd = 6 - currentWordContainerIndex;

        DataManager.instance.IncreaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 3);
    }
    public void BackspacePressedCallback()
    {
        if(!GameManager.instance.IsGameState())
            return;
        bool removedLetter = wordContainers[currentWordContainerIndex].RemoveLetter();
            
        if(removedLetter)
            DisableTryButton();
        canAddLetter = true;
    }

    void EnableTryButton()
    {
        tryButton.interactable = true;
    }
    void DisableTryButton()
    {
        tryButton.interactable = false;
    }
    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWordContainerIndex];
    }
}
