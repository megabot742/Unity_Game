using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] GameObject keyboard;
    KeyboardKey[] keys;
    void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void KeyboardHint()
    {
        //Debug.Log("Keyboard Hint");
        string secretWord = WordManager.instance.GetSecretWord();

        List<KeyboardKey> untouchedKeys = new List<KeyboardKey>();

        for(int i=0; i < keys.Length; i++)
        {
            if(keys[i].IsUntouched())
                untouchedKeys.Add(keys[i]);
        }

        List<KeyboardKey> t_untouchedKeys = new List<KeyboardKey>(untouchedKeys);

        for(int i=0; i< untouchedKeys.Count; i++)
        {
            if(secretWord.Contains(untouchedKeys[i].GetLetter()))
                t_untouchedKeys.Remove(untouchedKeys[i]);
        }

        if (t_untouchedKeys.Count <= 0)
            return;
        
        int randomKeyIndex = Random.Range(0, t_untouchedKeys.Count);
        t_untouchedKeys[randomKeyIndex].SetInvalid();
    }

    List<int> letterHintGivenIndices = new List<int>();
    public void LetterHint()
    {
        //Debug.Log("Letter Hint");
        if(letterHintGivenIndices.Count >= 5)
        {
            Debug.Log("All hints");
            return;
        }
        List<int> letterHintNotGivenIndices = new List<int>();

        for (int i=0; i < 5; i++)
            if(!letterHintGivenIndices.Contains(i))
                letterHintNotGivenIndices.Add(i);
        
        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();
        
        string secretWord = WordManager.instance.GetSecretWord();

        int randomIndex = letterHintNotGivenIndices[Random.Range(0, letterHintNotGivenIndices.Count)];
        letterHintGivenIndices.Add(randomIndex);

        currentWordContainer.AddAsHint(randomIndex, secretWord[randomIndex]);
    }
}
