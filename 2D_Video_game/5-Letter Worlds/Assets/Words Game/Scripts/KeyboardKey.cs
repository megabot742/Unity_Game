using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyboardKey : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] TextMeshProUGUI letterTex;

    [Header("Event")]
    public static Action<char> onKeyPressed;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterTex.text[0]);
    }
}
