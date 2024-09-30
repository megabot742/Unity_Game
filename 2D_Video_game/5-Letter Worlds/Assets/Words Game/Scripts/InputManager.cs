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

    [Header("Setting")]
    int currentWordContainerIndex;
    bool canAddLetter = true;
    // Start is called before the first frame update
    void Start()
    {
        Intialeize();
        KeyboardKey.onKeyPressed += KeyPressedCallback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Intialeize()
    {
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

        if(wordToCheck == secretWord)
            Debug.Log("Level Complete");
        else
        {
            Debug.Log("Wrong word");
            canAddLetter = true;
            DisableTryButton();
            currentWordContainerIndex++;
        }
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
