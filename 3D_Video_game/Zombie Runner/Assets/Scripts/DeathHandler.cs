using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

public class DeathHandler : MonoBehaviour
{
    [SerializeField] Canvas gameOverCanvas;
    void Start() 
    {
        gameOverCanvas.enabled = false;
    }
    public void HandleDeath()
    {
        GetComponent<FirstPersonController>().enabled = false;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
