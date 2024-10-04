using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] SpriteRenderer letterContainer;
    [SerializeField] TextMeshPro letter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Initialize()
    {
        letter.text = "";
        letterContainer.color = Color.white;
    }

    public void SetLetter(char letter, bool isHint = false)
    {
        if (isHint)
            this.letter.color = Color.gray;
        else
            this.letter.color = Color.black;

        this.letter.text = letter.ToString();
    }
    public void SetValid()
    {
        letterContainer.color = Color.green;
    }
    public void SetPotentail()
    {
        letterContainer.color = Color.yellow;
    }
    public void SetInvalid()
    {
        letterContainer.color = Color.gray;
    }
    public char GetLetter()
    {
        return letter.text[0];
    }
}
