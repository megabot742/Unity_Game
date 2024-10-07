using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] UnityEngine.UI.Image soundsImage;
    [SerializeField] UnityEngine.UI.Image hapticsImage;

    [Header("Setting")]
    bool soundsState; // false
    bool hapticsState; //false
    // Start is called before the first frame update
    void Start()
    {
        LoadStates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SoundsButtonCallback()
    {
        soundsState = !soundsState;
        UpdateSoundsState();
        SaveStates();
    }
    void UpdateSoundsState()
    {
        if(soundsState)
            EnableSounds();
        else
            DisableSounds();
    }
    void EnableSounds()
    {
        SoundsManager.instance.EnableSounds();
        soundsImage.color = Color.blue;
    }
    void DisableSounds()
    {
        SoundsManager.instance.DisableSounds();
        soundsImage.color = Color.gray;
    }
    //--------------Haptics------------//
    public void HapticsButtonCallback()
    {
        hapticsState = !hapticsState;
        UpdateHapticsState();
        SaveStates();
    }
    void UpdateHapticsState()
    {
        if(hapticsState)
            EnableHaptics();
        else
            DisableHaptics();
    }
    void EnableHaptics()
    {
        //HapticsManager.instance.EnableHaptics();
        hapticsImage.color = Color.blue;
    }
    void DisableHaptics()
    {
        //HapticsManager.instance.DisableHaptics();
        hapticsImage.color = Color.gray;
    }
    void LoadStates()
    {
        soundsState = PlayerPrefs.GetInt("sounds",1) == 1;
        hapticsState = PlayerPrefs.GetInt("haptics",1) == 1;

        UpdateSoundsState();
        UpdateHapticsState();
    }
    void SaveStates()
    {
        PlayerPrefs.SetInt("sounds", soundsState ? 1 : 0);
        PlayerPrefs.SetInt("haptics", hapticsState ? 1 : 0);
    }
}
