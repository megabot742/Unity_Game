using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] WordContainer[] wordContainers;

    [Header("Setting")]
    [SerializeField] int currentWordContainerIndex;
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
        if(wordContainers[currentWordContainerIndex].IsComplete())
            currentWordContainerIndex++;

        wordContainers[currentWordContainerIndex].Add(letter);
    }
}
