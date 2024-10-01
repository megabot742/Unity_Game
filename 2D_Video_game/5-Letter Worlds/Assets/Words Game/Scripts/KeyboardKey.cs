using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;

enum Validity {None, Valid, Potential, Invalid}

public class KeyboardKey : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] Image rendererImage;
    [SerializeField] TextMeshProUGUI letterTex;

    [Header("Setting")]
    Validity validity;

    [Header("Event")]
    public static Action<char> onKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
        Intialeize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterTex.text[0]);
    }

    public char GetLetter()
    {
        return letterTex.text[0];
    }

    public void Intialeize()
    {
        rendererImage.color = Color.white;
        validity = Validity.None;
    }
    public void SetValid()
    {
        rendererImage.color = Color.green;
        validity = Validity.Valid;
    }
    public void SetPotentail()
    {
        if(validity == Validity.Valid)
        {
            return;
        }
        rendererImage.color = Color.yellow;
        validity = Validity.Potential;
    }
    public void SetInvalid()
    {
        if(validity == Validity.Valid || validity == Validity.Potential)
            return;
        rendererImage.color = Color.gray;
        validity = Validity.Invalid;
    }
}
