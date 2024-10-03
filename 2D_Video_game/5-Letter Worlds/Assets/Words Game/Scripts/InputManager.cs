using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] WordContainer[] wordContainers;
    [SerializeField] Button tryButton;
    [SerializeField] KeyboardColorizer keyboardColorizer;

    [Header("Setting")]
    int currentWordContainerIndex;
    bool canAddLetter = true;
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
                Initialize();
                break;
            
            case GameState.LevelComplete:
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
            Debug.Log("Wrong word");
            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
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
}
