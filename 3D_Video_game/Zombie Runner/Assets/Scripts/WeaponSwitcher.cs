using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    [SerializeField] int currentWeapon = 0;
    void Start()
    {
        SetWeaponActive();
    }
    void Update()
    {
        //set weapon
        int previousWeapon = currentWeapon;
        //use keyboard for change weapon
        ProcessKeyInput();
        //use scroll wheel for change weapon
        ProcessScrollWheel();
        //update to correct weapon
        if(previousWeapon != currentWeapon)
        {
            SetWeaponActive();
        }
    }
    void SetWeaponActive()
    {
        int weaponIndex =  0;
        //Check weapon to active
        foreach (Transform weapon in transform)
        {
            //if correct weapon
            if (weaponIndex == currentWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            //wrong weapon
            else
            {
                weapon.gameObject.SetActive(false);
            }
            weaponIndex++;
        }
        
    }
    public void ProcessKeyInput()
    {
        //Button 1
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentWeapon = 0;
        }
        //Button 2
        else if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            currentWeapon = 1;
        }
        //Button 3
        else if(Input.GetKeyDown(KeyCode.Alpha3))
        {
            currentWeapon = 2;
        }
        //Button 4
        else if(Input.GetKeyDown(KeyCode.Alpha4))
        {
            currentWeapon = 3;
        }
    }
    public void ProcessScrollWheel()
    {
        //Scroll down
        if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            //check if get maximum weapon , then return first weapon
            if(currentWeapon >= transform.childCount - 1)
            {
                currentWeapon = 0;
            }
            //increase to next weapon
            else
            {
                currentWeapon++;
            }
        }
        //Scroll up
        else if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            //check if get minimum weapon , then return last weapon
            if(currentWeapon <= 0)
            {
                currentWeapon = transform.childCount - 1;
            }
            //decreased to next weapon
            else
            {
                currentWeapon--;
            }
        }
    }
    
}
