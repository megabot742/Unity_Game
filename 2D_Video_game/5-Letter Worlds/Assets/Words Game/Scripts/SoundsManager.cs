using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundsManager : MonoBehaviour
{
    public static SoundsManager instance;

    [Header("Sounds")]
    [SerializeField] AudioSource buttonSound;
    [SerializeField] AudioSource letterAddedSound;
    [SerializeField] AudioSource letterRemovedSound;
    [SerializeField] AudioSource levelCompleteSound;
    [SerializeField] AudioSource gameoverSound;

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
        InputManager.onLetterAdded += PlayLetterAddedSound;
        InputManager.onLetterRemoved += PlayLetterRemoveSound;

        GameManager.onGameStateChanged += GameStateChangedCallback;
    }
    void OnDestroy()
    {
        InputManager.onLetterAdded -= PlayLetterAddedSound;
        InputManager.onLetterRemoved -= PlayLetterRemoveSound;

        GameManager.onGameStateChanged -= GameStateChangedCallback;
    }
    void GameStateChangedCallback(GameState gameState)
    {
        switch(gameState)
        {
            case GameState.LevelComplete:
                levelCompleteSound.Play();
                break;
            
            case GameState.Gameover:
                gameoverSound.Play();
                break;    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayButtonSound()
    {
        buttonSound.Play();
    }
    void PlayLetterAddedSound()
    {
        letterAddedSound.Play();
    }
    void PlayLetterRemoveSound()
    {
        letterRemovedSound.Play();
    }
    public void EnableSounds()
    {
        buttonSound.volume = 1;
        letterAddedSound.volume = 1;
        letterRemovedSound.volume = 1;
        levelCompleteSound.volume = 1;
        gameoverSound.volume = 1;
    }
    public void DisableSounds()
    {
        buttonSound.volume = 0;
        letterAddedSound.volume = 0;
        letterRemovedSound.volume = 0;
        levelCompleteSound.volume = 0;
        gameoverSound.volume = 0;
    }
}
