using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{
    [Header("Elements")]
    KeyboardKey[] keys;
    void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for(int i = 0;i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();
            
            for(int j = 0; j < wordToCheck.Length; j++)
            {
                if(keyLetter != wordToCheck[j])
                    continue;
                
                //the key letter we're pressed is equals to the current wordToCheck letter

                if(keyLetter == secretWord[j])
                {
                    //Valid
                    keys[i].SetValid();
                }
                else if(secretWord.Contains(keyLetter))
                {
                    //Potential
                    keys[i].SetPotentail();
                }
                else
                {
                    //Invalid
                    keys[i].SetInvalid();
                }
            }
        }
    }
}
